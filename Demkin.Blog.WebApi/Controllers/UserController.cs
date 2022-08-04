using Demkin.Blog.DTO;
using Demkin.Blog.DTO.SysUser;
using Demkin.Blog.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Demkin.Blog.WebApi.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly ISysUserService _sysUserService;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="sysUserService"></param>
        public UserController(ILogger<UserController> logger, ISysUserService sysUserService)
        {
            _logger = logger;
            _sysUserService = sysUserService;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<PageModel<SysUserDetailDto>>> GetSysUserList(int currentPage, string key = "")
        {
            return ApiHelper.Failed<PageModel<SysUserDetailDto>>("A0001", "发生错误", null);
        }
    }
}