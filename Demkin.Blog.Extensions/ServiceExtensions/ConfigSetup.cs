using Demkin.Blog.Utils.Help;
using Demkin.Blog.Utils.SystemConfig;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Demkin.Blog.Extensions.ServiceExtensions
{
    /// <summary>
    /// 配置相关的服务
    /// </summary>
    public static class ConfigSetup
    {
        public static void AddConfigSetup(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton(new ConfigSetting(configuration, env));
            services.AddSingleton(new Appsettings(configuration));
        }
    }
}