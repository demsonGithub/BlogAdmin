using Demkin.Blog.DTO;
using Demkin.Blog.DTO.SysUser;
using Demkin.Blog.Entity;
using Demkin.Blog.IService.Base;
using Demkin.Blog.Utils.ClassExtension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Authorize(Policy = "AdminOrCommon")]
        [HttpGet]
        public async Task<ApiResponse<SysUser>> GetSysUser()
        {
            SysUser entity = await _sysUserService.GetEntityAsync(item => item.LoginAccount == "sysadmin");

            return ApiHelper.Success(entity);
        }

        [HttpGet]
        public async Task<ApiResponse<string>> GetException()
        {
            try
            {
                // 制造错误
                string temp = "中文汉字";
                int val = Convert.ToInt32(temp);

                return ApiHelper.Success(val.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetException");
                return ApiHelper.Failed(ApiErrorCode.Server_Error.GetDescription(), ex.Message);
            }
        }

        [HttpGet]
        public async Task<ApiResponse<List<SysUserDetailDto>>> GetSysUserList()
        {
            string sqlWhere = "";

            string orderByFiled = nameof(SysUserDetailDto.Age);

            var sysUsersDo = await _sysUserService.GetEntityListAsync(sqlWhere, orderByFiled);

            var result = (from sysUser in sysUsersDo
                          select new SysUserDetailDto
                          {
                              Id = sysUser.Id.ToString(),
                              NickName = sysUser.NickName,
                              Age = sysUser.Age,
                              CreateTime = sysUser.CreateTime
                          }).ToList();

            return ApiHelper.Success(result);
        }

        /// <summary>
        /// 直接停掉服务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CloseServer()
        {
            using (IServiceScope scope = HttpContext.RequestServices.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<IHostApplicationLifetime>();
                service.StopApplication();
            }
            return Ok();
        }

        [HttpPost]
        public async Task<ApiResponse<SysUser>> UpdateSysUser()
        {
            SysUser entity = await _sysUserService.GetEntityAsync(item => item.LoginAccount == "sysadmin");

            Random random = new Random();
            entity.Address = "湖北武汉" + random.Next(1, 1000);

            await _sysUserService.UpdateAsync(entity);

            return ApiHelper.Success(entity);
        }

        [HttpPost]
        public async Task<ApiResponse<string>> UpdateSysUserList()
        {
            Random random = new Random();
            var targetValue = "湖北武汉" + random.Next(1, 1000);

            await _sysUserService.UpdateAsync(obj => new SysUser { Address = targetValue }, item => item.Id > 0);

            return ApiHelper.Success("ok");
        }
    }
}