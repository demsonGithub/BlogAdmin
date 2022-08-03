using Demkin.Blog.DTO;
using Demkin.Blog.DTO.SysUser;
using Demkin.Blog.Entity;
using Demkin.Blog.IService.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demkin.Blog.WebApi.Controllers
{
    /// <summary>
    /// 测试用
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IBaseService<SysUser> _sysUserService;

        public TestController(IBaseService<SysUser> sysUserService)
        {
            _sysUserService = sysUserService;
        }

        /// <summary>
        /// 测试获取用户信息Dto
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<SysUserDto>> GetSysUserInfoTest(string account)
        {
            var sysUserBo = await _sysUserService.GetEntityAsync(item => item.LoginAccount == account);

            SysUserDto result = new SysUserDto
            {
                LoginAccount = sysUserBo?.LoginAccount,
                Id = (long)sysUserBo?.Id,
                NickName = sysUserBo?.NickName,
            };

            return ApiHelper.Success(result);
        }
    }
}