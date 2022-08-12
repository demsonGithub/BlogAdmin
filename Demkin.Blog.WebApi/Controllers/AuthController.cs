using AutoMapper;
using Demkin.Blog.DTO;
using Demkin.Blog.DTO.Auth;
using Demkin.Blog.DTO.RoleMenuPermissionRelation;
using Demkin.Blog.DTO.UserRoleRelation;
using Demkin.Blog.Entity;
using Demkin.Blog.IService;
using Demkin.Blog.Utils.ClassExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demkin.Blog.WebApi.Controllers
{
    /// <summary>
    /// 权限分配模块
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRoleRelationService _userRoleRelationService;
        private readonly IRoleMenuPermissionRelationService _roleMenuPermissionRelationService;

        public AuthController(ILogger<AuthController> logger, IMapper mapper,
            IUserRoleRelationService userRoleRelationService,
            IRoleMenuPermissionRelationService roleMenuPermissionRelationService)
        {
            _logger = logger;
            _mapper = mapper;
            _userRoleRelationService = userRoleRelationService;
            _roleMenuPermissionRelationService = roleMenuPermissionRelationService;
        }

        /// <summary>
        /// 给用户添加角色关系
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<string>> AddRoleToUser([FromBody] UserRoleRelationInsertDto entityDto)
        {
            var entity = new UserRoleRelation
            {
                UserId = entityDto.UserId,
                RoleId = entityDto.RoleId,
            };

            var result = await _userRoleRelationService.AddAsync(entity);
            if (result == null)
            {
                return ApiHelper.Failed(ApiErrorCode.Server_Error.GetDescription(), "添加失败");
            }
            return ApiHelper.Success();
        }

        /// <summary>
        /// 获取权限根据角色Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<List<RoleMenuPermissionRelationDetailDto>>> GetAuthListByRole(long roleId)
        {
            if (roleId == 0)
            {
                return ApiHelper.Failed<List<RoleMenuPermissionRelationDetailDto>>(ApiErrorCode.Client_Error.GetDescription(), "角色Id错误", null);
            }
            var result = await _roleMenuPermissionRelationService.GetRoleMenuPermissionMap(roleId);

            return ApiHelper.Success(result);
        }

        /// <summary>
        /// 给角色添加菜单、请求处理权限
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<string>> AddPermissionToRole([FromBody] RoleMenuPermissionRelationInsertDto entityDto)
        {
            if (entityDto.RoleId == 0)
            {
                return ApiHelper.Failed(ApiErrorCode.Client_Error.GetDescription(), "角色Id错误");
            }
            if (entityDto.MenuId == 0)
            {
                return ApiHelper.Failed(ApiErrorCode.Client_Error.GetDescription(), "菜单Id错误");
            }

            var entity = _mapper.Map<RoleMenuPermissionRelation>(entityDto);

            var result = await _roleMenuPermissionRelationService.AddAsync(entity);

            if (result == null)
            {
                ApiHelper.Failed(ApiErrorCode.Server_Error.GetDescription(), "发生了错误");
            }
            return ApiHelper.Success();
        }
    }
}