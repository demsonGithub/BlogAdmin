using AutoMapper;
using Demkin.Blog.DTO;
using Demkin.Blog.DTO.Role;
using Demkin.Blog.Entity;
using Demkin.Blog.IService;
using Demkin.Blog.Utils.ClassExtension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demkin.Blog.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public RoleController(ILogger<RoleController> logger, IMapper mapper, IRoleService roleService)
        {
            _logger = logger;
            _mapper = mapper;
            _roleService = roleService;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<List<RoleDetailDto>>> GetRoleList()
        {
            var roleListFromDo = await _roleService.GetEntityListAsync();

            var result = _mapper.Map<List<RoleDetailDto>>(roleListFromDo);

            return ApiHelper.Success(result);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<Role>> Add([FromBody] RoleInsertDto entityDto)
        {
            // 判断是否存在
            var isExistRole = await _roleService.IsExist(item => item.RoleName == entityDto.RoleName);

            if (isExistRole)
            {
                return ApiHelper.Failed<Role>(ApiErrorCode.Client_Error.GetDescription(), "角色已存在", null);
            }

            var entity = _mapper.Map<Role>(entityDto);

            var result = await _roleService.AddAsync(entity);

            if (result == null)
            {
                return ApiHelper.Failed<Role>(ApiErrorCode.Server_Error.GetDescription(), "发生了错误", null);
            }

            return ApiHelper.Success(result);
        }
    }
}