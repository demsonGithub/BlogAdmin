using Demkin.Blog.DTO;
using Demkin.Blog.DTO.Auth;
using Demkin.Blog.Enum;
using Demkin.Blog.Extensions.AuthRelated;
using Demkin.Blog.IService;
using Demkin.Blog.Utils.ClassExtension;
using Demkin.Blog.Utils.Help;
using Demkin.Blog.Utils.SystemConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml;

namespace Demkin.Blog.WebApi.Controllers
{
    /// <summary>
    /// 权限认证
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ISysUserService _sysUserService;
        private readonly PermissionRequirement _requirement;

        public LoginController(ILogger<LoginController> logger, ISysUserService sysUserService, PermissionRequirement requirement)
        {
            _logger = logger;
            _sysUserService = sysUserService;
            _requirement = requirement;
        }

        /// <summary>
        /// 获取错误码的注释
        /// </summary>
        /// <param name="fileSuffix">文件的后缀，（pdf，xls，xlsx）</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetErrorCodeDesciption(string fileSuffix)
        {
            if (!(fileSuffix.ToLower() == "pdf" || fileSuffix.ToLower() == "xls" || fileSuffix.ToLower() == "xlsx"))
            {
                return new JsonResult(ApiHelper.Failed(ApiErrorCode.Client_RequestParam.GetDescription(), "文件名后缀不支持"));
            }

            string filePath = Path.Combine(AppContext.BaseDirectory, ConfigSetting.SiteInfo.DtoDllName + ".xml");
            if (!System.IO.File.Exists(filePath))
            {
                return new JsonResult(ApiHelper.Failed(ApiErrorCode.Client_RequestParam.GetDescription(), "注释文件不存在"));
            }
            string fullName = ConfigSetting.SiteInfo.DtoDllName + ".ApiErrorCode";

            try
            {
                XmlDocument xmlDocument = XmlHelper.XMLFromFilePath(filePath);

                var errCodeDtoList = new List<ApiErrorCodeDto>();
                foreach (XmlElement xmlElement in xmlDocument["doc"]["members"])
                {
                    if (!xmlElement.Attributes["name"].Value.Contains(fullName))
                    {
                        continue;
                    }
                    if (xmlElement.Attributes["name"].Value.Equals("T:" + fullName))
                    {
                        ApiErrorCodeDto headColumn = new ApiErrorCodeDto
                        {
                            ErrorCode = "注释",
                            ErrorDescription = xmlElement["summary"].InnerText.Trim()
                        };
                        errCodeDtoList.Add(headColumn);
                        continue;
                    }
                    var tempEnumValue = xmlElement.Attributes["name"].Value.Substring(2).Replace(fullName + ".", "");

                    var intEnum = EnumExtension.GetEnum<ApiErrorCode>(tempEnumValue).GetDescription();

                    ApiErrorCodeDto itemColumn = new ApiErrorCodeDto
                    {
                        ErrorCode = intEnum,
                        ErrorDescription = xmlElement["summary"].InnerText.Trim()
                    };
                    errCodeDtoList.Add(itemColumn);
                }
                DataTable dt = errCodeDtoList.ToDataTable();

                if (fileSuffix.ToLower() == "pdf")
                {
                    var contentBytes = PdfHelper.DataTableToPDF(dt, 18);
                    return File(contentBytes, "application/octet-stream", "接口错误码.pdf");
                }
                else if (fileSuffix.ToLower() == "xls" || fileSuffix.ToLower() == "xlsx")
                {
                    var contentBytes = NPOIHelper.DataTableToExcel(dt, fileSuffix);
                    return File(contentBytes, "application/octet-stream", "接口错误码." + fileSuffix.ToLower());
                }
                else
                {
                    return new JsonResult(ApiHelper.Failed(ApiErrorCode.Server_Error.GetDescription(), "发生了错误"));
                }
            }
            catch
            {
                return new JsonResult(ApiHelper.Failed(ApiErrorCode.Server_Error.GetDescription(), "服务端发生了错误"));
            }
        }

        /// <summary>
        /// 登录获取token信息
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<TokenDetailDto>> GetJwtToken(string account = "", string password = "")
        {
            try
            {
                if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
                    return ApiHelper.Failed<TokenDetailDto>(ApiErrorCode.Client_Login.GetDescription(), "账号或密码必填", null);

                password = MD5Helper.MD5Encrypt32(password);

                var sysUserDo = await _sysUserService.GetEntityAsync(item => item.LoginAccount == account && item.LoginPwd == password && item.Status == Status.Enable);
                if (sysUserDo == null)
                {
                    return ApiHelper.Failed<TokenDetailDto>(ApiErrorCode.Client_Login.GetDescription(), "账号或密码错误", null);
                }
                // 获取角色
                var roles = await _sysUserService.GetUserRoles(sysUserDo.Id);

                // 声明Claim，配置用户标识
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,sysUserDo.LoginAccount),
                    new Claim(JwtRegisteredClaimNames.Jti,sysUserDo.Id.ToString()),
                    new Claim(ClaimTypes.Expiration,DateTime.Now.AddMinutes(_requirement.ExpiresTime.Minutes).ToString())
                };
                // 添加角色,角色对应的url
                claims.AddRange(roles.Split(',').Select(x => new Claim(ClaimTypes.Role, x)));

                string token = JwtTokenHandler.BuildJwtToken(claims.ToArray(), _requirement);

                var resultDto = new TokenDetailDto()
                {
                    Token = token,
                    ExpirationDate = DateTime.Now.AddMinutes(ConfigSetting.JwtTokenInfo.ExpiresTime).ToString("yyyy-MM-dd HH:mm:ss"),
                    TokenType = "Bearer"
                };

                return ApiHelper.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ApiHelper.Failed<TokenDetailDto>(ApiErrorCode.Server_Error.GetDescription(), ex.Message, null);
            }
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="originalToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public async Task<ApiResponse<TokenDetailDto>> RefreshJwtToken(string originalToken)
        {
            // todo 刷新token
            throw new NotImplementedException();
        }
    }
}