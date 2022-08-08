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

                // reloadOnChange �ȸ���
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

                // 1. ��Host��ӻ���Web��Ӧ��
                builder = builder.ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

                // 2. �������������������
                builder.ConfigureHostConfiguration(configBuilder =>
                {
                });

                // 3. Ϊ���ɹ��̺�Ӧ�ó�������ಿ����������
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

                // 4. ��������ӷ��� �ɶ�ν��е���
                builder.ConfigureServices(services =>
                {
                });

                // 5. ���һ��ί���������ṩ��ILoggingBuilder
                builder.ConfigureLogging(logBuilder =>
                {
                });

                // 6. ��������ע������
                builder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureContainer<ContainerBuilder>(builder =>
                    {
                        builder.RegisterModule(new AutofacModuleRegister());
                    });

                #region ��չ

                // ʹ��Serilog����־���
                builder.UseSerilog(dispose: true);

                // �ж������window����ʹ�÷���ķ�ʽ����
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // Nuget: Microsoft.Extensions.Hosting.WindowsServices
                    builder.UseWindowsService();
                }

                #endregion ��չ

                builder.Build().Run();
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
    }
}