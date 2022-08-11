using Demkin.Blog.Utils.SystemConfig;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Demkin.Blog.Extensions.ServiceExtensions
{
    public static class CorsSetup
    {
        public static void AddCorsSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddCors(options =>
            {
                // 允许任意跨域请求
                options.AddPolicy(ConfigSetting.SiteInfo.CorsPolicyName, policy =>
                {
                    if (ConfigSetting.SiteInfo.CorsPolicyName == "All")
                    {
                        policy.SetIsOriginAllowed(host => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    }
                    else
                    {
                        policy.WithOrigins(ConfigSetting.SiteInfo.AllowCorsIP.Split(','))
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    }
                });
            });
        }
    }
}