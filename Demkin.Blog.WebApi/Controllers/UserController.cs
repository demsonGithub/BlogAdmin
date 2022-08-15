using AutoMapper;
using Demkin.Blog.DTO;
using Demkin.Blog.DTO.SysUser;
using Demkin.Blog.Extensions.AuthRelated;
using Demkin.Blog.IService;
using Demkin.Blog.Utils.ClassExtension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

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
        private readonly IMapper _mapper;
        private readonly ISysUserService _sysUserService;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="sysUserService"></param>
        public UserController(ILogger<UserController> logger, IMapper mapper, ISysUserService sysUserService)
        {
            _logger = logger;
            _mapper = mapper;
            _sysUserService = sysUserService;
        }

        /// <summary>
        /// 根据token获取账号的详情
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<SysUserDetailDto>> GetUserInfoByToken(string token)
        {
            if (token == null)
            {
                return ApiHelper.Failed<SysUserDetailDto>(ApiErrorCode.Client_Error.GetDescription(), "token参数为null", null);
            }
            var tokenDetailModel = JwtTokenHandler.SerializeJwtToken(token);
            if (tokenDetailModel == null || string.IsNullOrEmpty(tokenDetailModel.JwtId))
            {
                return ApiHelper.Failed<SysUserDetailDto>(ApiErrorCode.Client_Error.GetDescription(), "token解析失败", null);
            }
            var sysUserFromDo = await _sysUserService.GetEntityAsync(tokenDetailModel.JwtId);
            if (sysUserFromDo == null)
            {
                return ApiHelper.Failed<SysUserDetailDto>(ApiErrorCode.Client_Error.GetDescription(), "未查询到该用户", null);
            }
            var result = _mapper.Map<SysUserDetailDto>(sysUserFromDo);
            result.Roles = tokenDetailModel.Role;

            return ApiHelper.Success(result);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<PageModel<SysUserDetailDto>>> GetUserList(int currentPage, string key = "")
        {
            // todo
            throw new NotImplementedException();
        }
    }
}