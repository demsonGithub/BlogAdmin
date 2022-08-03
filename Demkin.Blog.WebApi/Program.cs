using Autofac.Extensions.DependencyInjection;
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
                // ÐÞ¸ÄÄ¬ÈÏµÄÈÕÖ¾¿ò¼Ü
                builder.UseSerilog();
                // ¸ü»»ÒÀÀµ×¢ÈëÈÝÆ÷
                builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());

                builder.Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Æô¶¯Ê§°Ü");
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