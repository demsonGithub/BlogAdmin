using Demkin.Blog.DTO;
using Demkin.Blog.DTO.Menu;
using Demkin.Blog.Entity;
using Demkin.Blog.IService;
using Demkin.Blog.Utils.ClassExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IMenuService _menuService;

        public MenuController(ILogger<MenuController> logger, IMenuService menuService)
        {
            _logger = logger;
            _menuService = menuService;
        }

        [HttpPost]
        public async Task<ApiResponse<Menu>> AddMenu([FromBody] MenuInsertDto entityDto)
        {
            var isExistMenu = await _menuService.IsExist(item => item.ParentId == entityDto.ParentId && item.MenuName == entityDto.MenuName);
            if (isExistMenu)
            {
                return ApiHelper.Failed<Menu>(ApiErrorCode.Client_Error.GetDescription(), "已存在当前菜单", null);
            }

            var entity = new Menu { };

            // var result = await _menuService.AddAsync(entity);
            return ApiHelper.Failed<Menu>(ApiErrorCode.Client_Error.GetDescription(), "发生错误", null);
        }
    }
}