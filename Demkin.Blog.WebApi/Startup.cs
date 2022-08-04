using Autofac;
using Demkin.Blog.DbAccess;
using Demkin.Blog.DbAccess.UnitOfWork;
using Demkin.Blog.Extensions.Middlewares;
using Demkin.Blog.Extensions.ServiceExtensions;
using Demkin.Blog.IService.Base;
using Demkin.Blog.Repository.Base;
using Demkin.Blog.Service.Base;
using Demkin.Blog.Utils.Help;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Demkin.Blog.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //修改属性名称的序列化方式，首字母小写，即驼峰样式
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //解决命名不一致问题
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //忽略空值处理
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddSingleton(new Appsettings(Configuration));

            services.AddSwaggerSetup();

            // SqlSugar
            services.AddSqlSugarSetup();
            services.AddScoped<MyDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyDbContext myDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerMiddleware();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseInitBasicDataMiddleware(myDbContext, env.WebRootPath);
        }
    }
}