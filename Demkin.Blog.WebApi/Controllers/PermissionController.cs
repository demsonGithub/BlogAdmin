using AutoMapper;
using Demkin.Blog.DTO;
using Demkin.Blog.DTO.Permission;
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
    /// 请求处理 等模块
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly ILogger<PermissionController> _logger;
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;

        public PermissionController(ILogger<PermissionController> logger, IMapper mapper, IPermissionService permissionService)
        {
            _logger = logger;
            _mapper = mapper;
            _permissionService = permissionService;
        }

        /// <summary>
        /// 查看所有的接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<List<Permission>>> GetAllPermission()
        {
            var result = await _permissionService.GetEntityListAsync();

            return ApiHelper.Success(result);
        }

        /// <summary>
        /// 新增按钮、链接
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<Permission>> Add([FromBody] PermissionInsertDto entityDto)
        {
            if (entityDto.MenuId == 0)
            {
                return ApiHelper.Failed<Permission>(ApiErrorCode.Client_Error.GetDescription(), "所属菜单Id参数错误", null);
            }

            var isExistPermission = await _permissionService.IsExist(item => item.ActionName == entityDto.ActionName && item.MenuId == entityDto.MenuId);

            if (isExistPermission)
            {
                return ApiHelper.Failed<Permission>(ApiErrorCode.Client_Error.GetDescription(), "当前按钮或链接已存在", null);
            }
            var entity = _mapper.Map<Permission>(entityDto);
            var result = await _permissionService.AddAsync(entity);

            if (result == null)
            {
                return ApiHelper.Failed<Permission>(ApiErrorCode.Client_Error.GetDescription(), "当前按钮或链接已存在", null);
            }
            return ApiHelper.Success(result);
        }
    }
}