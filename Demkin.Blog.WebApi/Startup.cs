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
                // 全局异常过滤
                o.Filters.Add(typeof(GlobalExceptionsFilter));
            }).AddNewtonsoftJson(options =>
            {
                //修改属性名称的序列化方式，首字母小写，即驼峰样式
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //如果属性名不希望驼峰样式，那就使用默认，然后在返回实体上标注，eg：[Newtonsoft.Json.JsonProperty("code")]
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //忽略空值处理
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddConfigSetup(Configuration, Env);

            services.AddSwaggerSetup();

            // SqlSugar
            services.AddSqlSugarSetup();
            services.AddScoped<MyDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // jwt认证
            services.AddAuthentication_JwtSetup();
            // 自定义授权
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
            //            // 将错误Http响应码设置为500
            //            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //        }
            //        else
            //        {
            //            // 是已知的异常
            //            knownException = KnownException.FromKnownException(knownException);
            //            // 将Http响应码设置为200
            //            context.Response.StatusCode = StatusCodes.Status200OK;
            //        }
            //        var jsonOptions = context.RequestServices.GetService<IOptions<JsonOptions>>();
            //        context.Response.ContentType = "application/json;charset=utf-8";
            //        //await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(knownException, jsonOptions.Value.JsonSerializerOptions));
            //        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(ApiHelper.Failed("CCCC", "出错了")));
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