using AutoMapper;
using Demkin.Blog.DTO;
using Demkin.Blog.DTO.MenuPermission;
using Demkin.Blog.Entity;
using Demkin.Blog.Enum;
using Demkin.Blog.Extensions.AuthRelated;
using Demkin.Blog.IService;
using Demkin.Blog.Utils.ClassExtension;
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
    public class MenuPermissionController : ControllerBase
    {
        private readonly ILogger<MenuPermissionController> _logger;
        private readonly IMapper _mapper;
        private readonly IMenuPermissionService _menuPermissionService;

        public MenuPermissionController(ILogger<MenuPermissionController> logger, IMapper mapper,
            IMenuPermissionService menuPermissionService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _menuPermissionService = menuPermissionService;
        }

        /// <summary>
        /// 添加新菜单或按钮
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<MenuPermission>> AddMenuPermission([FromBody] MenuPermissionInsertDto entityDto)
        {
            if ((entityDto.LinkType == LinkType.Menu && string.IsNullOrEmpty(entityDto.LinkUrl)) ||
                (entityDto.LinkType == LinkType.Catalog && string.IsNullOrEmpty(entityDto.LinkUrl)) ||
                (entityDto.LinkType == LinkType.Button && string.IsNullOrEmpty(entityDto.ActionUrl)))
            {
                return ApiHelper.Failed<MenuPermission>(ApiErrorCode.Client_Error.GetDescription(), "添加项类型的url不可为空", null);
            }

            var isExistMenu = await _menuPermissionService.IsExist(item => item.ParentId == entityDto.ParentId && item.Name == entityDto.Name);
            if (isExistMenu)
            {
                return ApiHelper.Failed<MenuPermission>(ApiErrorCode.Client_Error.GetDescription(), "已存在当前菜单或按钮", null);
            }

            var entity = _mapper.Map<MenuPermission>(entityDto);

            var result = await _menuPermissionService.AddAsync(entity);

            if (result == null)
            {
                return ApiHelper.Failed<MenuPermission>(ApiErrorCode.Client_Error.GetDescription(), "发生错误", null);
            }
            return ApiHelper.Success(result);
        }

        /// <summary>
        /// 根据Id删除菜单
        /// </summary>
        /// <param name="menuPermissionId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResponse<string>> DeleteMenuPermissionById(long menuPermissionId)
        {
            var result = await _menuPermissionService.DeleteAsync(menuPermissionId);

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
        public async Task<ApiResponse<string>> DeleteMenuPermissionByIdList(List<long> menuPermissionIds)
        {
            var resultNum = await _menuPermissionService.DeleteAsync(item => menuPermissionIds.Contains(item.Id));

            if (resultNum == 0)
            {
                return ApiHelper.Failed(ApiErrorCode.Server_Error.GetDescription(), "发生错误");
            }
            return ApiHelper.Success();
        }

        /// <summary>
        /// 查询目录以及菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<List<MenuPermissionDetailDto>>> GetMenuPermissionList()
        {
            var menuFromDo = await _menuPermissionService.GetEntityListAsync(item => item.Status == Status.Enable);

            var result = _mapper.Map<List<MenuPermissionDetailDto>>(menuFromDo);

            result = FormatListToTree(result, 0);

            return ApiHelper.Success(result);
        }

        /// <summary>
        /// 查询目录下的子菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<List<MenuPermissionDetailDto>>> GetMenuListByParentId(long pId)
        {
            var menuFromDo = await _menuPermissionService.GetEntityListAsync(
                item => item.ParentId == pId
                && (item.LinkType == LinkType.Catalog || item.LinkType == LinkType.Menu)
                && item.Status == Status.Enable);

            var result = _mapper.Map<List<MenuPermissionDetailDto>>(menuFromDo);

            return ApiHelper.Success(result);
        }

        #region 私有方法

        private List<MenuPermissionDetailDto> FormatListToTree(
            List<MenuPermissionDetailDto> sourceList, long pId)
        {
            var targetList = new List<MenuPermissionDetailDto>();
            // 找出所有的父节点
            var pItems = sourceList.Where(item => item.ParentId == pId).ToList();

            // 根据父节点去递归寻找字节的

            foreach (var item in pItems)
            {
                var childItems = FormatListToTree(sourceList, item.Id);

                item.Children = childItems;

                targetList.Add(item);
            }

            return targetList;
        }

        #endregion 私有方法
    }
}