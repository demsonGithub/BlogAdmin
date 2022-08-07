using Autofac.Extensions.DependencyInjection;
using Demkin.Blog.Utils.Help;
using Demkin.Blog.Utils.SystemConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Serilog;
using System;
using System.IO;
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
                IConfiguration config = null;
                if (environment == "Development")
                {
                    // reloadOnChange �ȸ���
                    config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + $"\\appsettings.{environment}.json", optional: false, reloadOnChange: true)
                        .Build();
                }
                else
                {
                    config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "\\appsettings.json", optional: false, reloadOnChange: true)
                        .Build();
                }

                Log.Logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(config)
                         .CreateLogger();

                var builder = CreateHostBuilder(args);

                // ��������ע������
                builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());

                // �ж������window����ʹ�÷���ķ�ʽ����
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // Nuget: Microsoft.Extensions.Hosting.WindowsServices
                    builder.UseWindowsService();
                }
                // �޸�Ĭ�ϵ���־���
                builder.UseSerilog();
                var host = builder.Build();

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "����ʧ��");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                // ���һ�����������Ƿ��ȸ���
                config.AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "\\appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(args.Length == 0 ? $"http://*:8090" : $"http://*:{args[0]}");
                    webBuilder.UseStartup<Startup>();
                })
            ;
    }
}