using Autofac;
using Demkin.Blog.DbAccess;
using Demkin.Blog.DbAccess.UnitOfWork;
using Demkin.Blog.DTO;
using Demkin.Blog.Extensions.Exceptions;
using Demkin.Blog.Extensions.Filter;
using Demkin.Blog.Extensions.Middlewares;
using Demkin.Blog.Extensions.ServiceExtensions;
using Demkin.Blog.Utils.Help;
using Demkin.Blog.Utils.SystemConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Demkin.Blog.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(o =>
            {
                // ȫ���쳣����
                o.Filters.Add(typeof(GlobalExceptionsFilter));
            }).AddNewtonsoftJson(options =>
            {
                //�޸��������Ƶ����л���ʽ������ĸСд�����շ���ʽ
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //�����������ϣ���շ���ʽ���Ǿ�ʹ��Ĭ�ϣ�Ȼ���ڷ���ʵ���ϱ�ע��eg��[Newtonsoft.Json.JsonProperty("code")]
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //����ʱ���ʽ
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //���Կ�ֵ����
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddConfigSetup(Configuration, Env);

            services.AddSwaggerSetup();

            // SqlSugar
            services.AddSqlSugarSetup();
            services.AddScoped<MyDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // jwt��֤
            services.AddAuthentication_JwtSetup();
            // �Զ�����Ȩ
            services.AddAuthorizationSetup();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyDbContext myDbContext, ILoggerFactory loggerFactory)
        {
            //if (env.IsDevelopment())
            //{
            //app.UseDeveloperExceptionPage();
            app.UseSwaggerMiddleware();
            //}

            //app.UseExceptionHandler(error =>
            //{
            //    error.Run(async context =>
            //    {
            //        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            //        var knownException = exceptionHandlerPathFeature.Error as IKnownException;
            //        if (knownException == null)
            //        {
            //            var logger = loggerFactory.CreateLogger("Api.Exception");
            //            logger.LogError(exceptionHandlerPathFeature.Error, exceptionHandlerPathFeature.Error.Message);

            //            knownException = KnownException.UnKnown;
            //            // ������Http��Ӧ������Ϊ500
            //            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //        }
            //        else
            //        {
            //            // ����֪���쳣
            //            knownException = KnownException.FromKnownException(knownException);
            //            // ��Http��Ӧ������Ϊ200
            //            context.Response.StatusCode = StatusCodes.Status200OK;
            //        }
            //        var jsonOptions = context.RequestServices.GetService<IOptions<JsonOptions>>();
            //        context.Response.ContentType = "application/json;charset=utf-8";
            //        //await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(knownException, jsonOptions.Value.JsonSerializerOptions));
            //        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(ApiHelper.Failed("CCCC", "������")));
            //    });
            //});

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // app.UseInitBasicDataMiddleware(myDbContext, env.WebRootPath);
        }
    }
}