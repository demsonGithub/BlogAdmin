using Autofac;
using Autofac.Extensions.DependencyInjection;
using Demkin.Blog.Extensions.ServiceExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Runtime.InteropServices;

namespace Demkin.Blog.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                // reloadOnChange 热更新
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                                .Build();

                Log.Logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(config)
                         .Enrich.FromLogContext()
                         .CreateLogger();

                var builder = Host.CreateDefaultBuilder(args);

                // 1. 给Host添加基于Web的应用
                builder = builder.ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

                // 2. 设置生成器自身的配置
                builder.ConfigureHostConfiguration(configBuilder =>
                {
                });

                // 3. 为生成过程和应用程序的其余部分设置配置
                builder.ConfigureAppConfiguration((context, configBuilder) =>
                {
                    configBuilder.Sources.Clear();

                    var env = context.HostingEnvironment;

                    configBuilder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddCommandLine(args)
                    .AddEnvironmentVariables();
                });

                // 4. 向容器添加服务。 可多次进行调用
                builder.ConfigureServices(services =>
                {
                });

                // 5. 添加一个委托来配置提供的ILoggingBuilder
                builder.ConfigureLogging(logBuilder =>
                {
                });

                // 6. 更换依赖注入容器
                builder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureContainer<ContainerBuilder>(builder =>
                    {
                        builder.RegisterModule(new AutofacModuleRegister());
                    });

                #region 扩展

                // 使用Serilog的日志框架
                builder.UseSerilog(dispose: true);

                // 判断如果是window，可使用服务的方式启动
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // Nuget: Microsoft.Extensions.Hosting.WindowsServices
                    builder.UseWindowsService();
                }

                #endregion 扩展

                builder.Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "启动失败");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}