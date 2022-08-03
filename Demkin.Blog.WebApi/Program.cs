using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;

namespace Demkin.Blog.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                IConfigurationRoot config = null;
                if (environment == "Development")
                {
                    config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true).Build();
                }
                else
                {
                    config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                        .Build();
                }
                Log.Logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(config)
                         .CreateLogger();

                var builder = CreateHostBuilder(args);
                // 修改默认的日志框架
                builder.UseSerilog();

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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}