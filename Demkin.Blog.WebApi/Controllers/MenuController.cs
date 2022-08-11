using AutoMapper;
using Demkin.Blog.DTO;
using Demkin.Blog.DTO.Menu;
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
    /// 菜单模块
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ILogger<MenuController> _logger;
        private readonly IMapper _mapper;
        private readonly IMenuService _menuService;

        public MenuController(ILogger<MenuController> logger, IMapper mapper, IMenuService menuService)
        {
            _logger = logger;
            _mapper = mapper;
            _menuService = menuService;
        }

        /// <summary>
        /// 查询所有的菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<List<Menu>>> GetAllMenu()
        {
            var result = await _menuService.GetEntityListAsync();

            return ApiHelper.Success(result);
        }

        /// <summary>
        /// 添加新菜单
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<Menu>> Add([FromBody] MenuInsertDto entityDto)
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
    }
}