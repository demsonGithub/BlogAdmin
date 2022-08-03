using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
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
            });
        }
    }
}