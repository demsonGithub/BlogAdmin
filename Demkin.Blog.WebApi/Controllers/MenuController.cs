using AutoMapper;
using Demkin.Blog.DTO;
using Demkin.Blog.DTO.Menu;
using Demkin.Blog.DTO.Permission;
using Demkin.Blog.Entity;
using Demkin.Blog.IService;
using Demkin.Blog.Utils.ClassExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demkin.Blog.WebApi.Controllers
{
    /// <summary>
    /// 菜单，按钮等模块
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ILogger<MenuController> _logger;
        private readonly IMapper _mapper;
        private readonly IMenuService _menuService;
        private readonly IPermissionService _permissionService;

        public MenuController(ILogger<MenuController> logger, IMapper mapper,
            IMenuService menuService,
            IPermissionService permissionService)
        {
            _logger = logger;
            _mapper = mapper;
            _menuService = menuService;
            _permissionService = permissionService;
        }

        #region 菜单

        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<List<MenuDetailDto>>> GetMenuList()
        {
            var menuFromDo = await _menuService.GetEntityListAsync();

            var result = _mapper.Map<List<MenuDetailDto>>(menuFromDo);

            return ApiHelper.Success(result);
        }

        /// <summary>
        /// 添加新菜单
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<Menu>> AddMenu([FromBody] MenuInsertDto entityDto)
        {
            var isExistMenu = await _menuService.IsExist(item => item.ParentId == entityDto.ParentId && item.MenuName == entityDto.MenuName);
            if (isExistMenu)
            {
                return ApiHelper.Failed<Menu>(ApiErrorCode.Client_Error.GetDescription(), "已存在当前菜单", null);
            }

            var entity = _mapper.Map<Menu>(entityDto);

            var result = await _menuService.AddAsync(entity);

            if (result == null)
            {
                return ApiHelper.Failed<Menu>(ApiErrorCode.Client_Error.GetDescription(), "发生错误", null);
            }
            return ApiHelper.Success(result);
        }

        /// <summary>
        /// 根据Id删除菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResponse<string>> DeleteMenuById(long menuId)
        {
            var result = await _menuService.DeleteAsync(menuId);

            if (!result)
            {
                return ApiHelper.Failed(ApiErrorCode.Server_Error.GetDescription(), "发生错误");
            }
            return ApiHelper.Success();
        }

        /// <summary>
        /// 根据Id列表删除菜单
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResponse<string>> DeleteMenuByIdList(List<long> menuIds)
        {
            var resultNum = await _menuService.DeleteAsync(item => menuIds.Contains(item.Id));

            if (resultNum == 0)
            {
                return ApiHelper.Failed(ApiErrorCode.Server_Error.GetDescription(), "发生错误");
            }
            return ApiHelper.Success();
        }

        #endregion 菜单

        #region 按钮

        /// <summary>
        /// 查看按钮列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<List<PermissionDetailDto>>> GetPermissionList()
        {
            var permissionListFromDo = await _permissionService.GetEntityListAsync();

            var result = _mapper.Map<List<PermissionDetailDto>>(permissionListFromDo);

            return ApiHelper.Success(result);
        }

        /// <summary>
        /// 新增按钮、链接
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<Permission>> AddPermission([FromBody] PermissionInsertDto entityDto)
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

        #endregion 按钮
    }
}