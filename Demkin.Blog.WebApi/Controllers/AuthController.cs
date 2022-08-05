using Demkin.Blog.DTO;
using Demkin.Blog.DTO.Auth;
using Demkin.Blog.Utils.ClassExtension;
using Demkin.Blog.Utils.Help;
using Demkin.Blog.Utils.SystemConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace Demkin.Blog.WebApi.Controllers
{
    /// <summary>
    /// 权限认证
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取错误码的注释
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<List<ApiErrorCodeDto>>> GetErrorCodeDesciption()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, SiteInfo.DtoDllName + ".xml");
            if (!System.IO.File.Exists(filePath))
            {
                return ApiHelper.Failed<List<ApiErrorCodeDto>>(ApiErrorCode.Server_Resource.GetDescription(), "注释文档文件不存在", null);
            }
            string fullName = SiteInfo.DtoDllName + ".ApiErrorCode";

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
                DataTable dt = ListToTable(errCodeDtoList);

                NPOIHelper.DataTableToExcel(@"D:\\test.xls", dt);

                return ApiHelper.Success(errCodeDtoList);
            }
            catch
            {
                return ApiHelper.Failed<List<ApiErrorCodeDto>>(ApiErrorCode.Server_Error.GetDescription(), "出错了", null);
            }
        }

        public static DataTable ListToTable<T>(List<T> list)
        {
            Type tp = typeof(T);
            PropertyInfo[] proInfos = tp.GetProperties();
            DataTable dt = new DataTable();
            foreach (var item in proInfos)
            {
                dt.Columns.Add(item.Name, item.PropertyType); //添加列明及对应类型
            }
            foreach (var item in list)
            {
                DataRow dr = dt.NewRow();
                foreach (var proInfo in proInfos)
                {
                    object obj = proInfo.GetValue(item);
                    if (obj == null)
                    {
                        continue;
                    }
                    if (proInfo.PropertyType == typeof(DateTime) && Convert.ToDateTime(obj) < Convert.ToDateTime("1753-01-01"))
                    {
                        continue;
                    }
                    // dr[proInfo.Name] = proInfo.GetValue(item);
                    dr[proInfo.Name] = obj;
                    // }
                }
                dt.Rows.Add(dr);
            }
            return dt;
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
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
                return ApiHelper.Failed<TokenDetailDto>(ApiErrorCode.Success.GetDescription(), "账号或密码错误", null);

            return ApiHelper.Failed<TokenDetailDto>("A0002", "发生错误", null);
        }
    }
}