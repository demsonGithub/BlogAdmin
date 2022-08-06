using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;

namespace Demkin.Blog.Extensions.ServiceExtensions
{
    /// <summary>
    /// Swagger 服务配置
    /// </summary>
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            string basePath = AppContext.BaseDirectory;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CMS 接口文档",
                    Description = "CMS 接口文档 v1"
                });
                c.OrderActionsBy(x => x.RelativePath);
                try
                {
                    string xmlPath = Path.Combine(basePath, "Demkin.Blog.WebApi.xml");
                    c.IncludeXmlComments(xmlPath, true);

                    string dtoXmlPath = Path.Combine(basePath, "Demkin.Blog.DTO.xml");
                    c.IncludeXmlComments(dtoXmlPath);
                }
                catch (Exception)
                {
                    Log.Warning("xml注释文件不存在");
                }

                // 开启加权的锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // 在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                // Jwt Bearer 认证，必须是oauth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });
        }
    }
}