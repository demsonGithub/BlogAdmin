using Demkin.Blog.DbAccess;
using Demkin.Blog.DbAccess.UnitOfWork;
using Demkin.Blog.Extensions.Filter;
using Demkin.Blog.Extensions.Middlewares;
using Demkin.Blog.Extensions.ServiceExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerMiddleware();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // app.UseInitBasicDataMiddleware(myDbContext, env.WebRootPath);
        }
    }
}