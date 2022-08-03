using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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
                    string xmlPath = Path.Combine(basePath, "Demkin.CMS.WebApi.xml");
                    c.IncludeXmlComments(xmlPath, true);
                }
                catch (Exception)
                {
                }
            });
        }
    }
}