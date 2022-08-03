using Demkin.Blog.DTO;
using Demkin.Blog.DTO.SysUser;
using Demkin.Blog.Entity;
using Demkin.Blog.IService.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly ILogger<TestController> _logger;
        private readonly IBaseService<SysUser> _sysUserService;

        public TestController(ILogger<TestController> logger, IBaseService<SysUser> sysUserService)
        {
            _logger = logger;
            _sysUserService = sysUserService;
        }

        /// <summary>
        /// 测试获取用户信息Dto
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<SysUserDto>> GetSysUserInfoTest(string account)
        {
            _logger.LogInformation("开始执行");
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetSysUserInfoTest出现异常");
                return ApiHelper.Failed<SysUserDto>("A0002", ex.Message, null);
            }
        }
    }
}