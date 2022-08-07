using Autofac;
using Demkin.Blog.DbAccess;
using Demkin.Blog.DbAccess.UnitOfWork;
using Demkin.Blog.Extensions.Filter;
using Demkin.Blog.Extensions.Middlewares;
using Demkin.Blog.Extensions.ServiceExtensions;
using Demkin.Blog.Utils.Help;
using Demkin.Blog.Utils.SystemConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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
            services.AddControllers(o =>
            {
                // ȫ���쳣���� o.Filters.Add(typeof(GlobalExceptionsFilter));
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
            services.AddSingleton(new ConfigSetting(Configuration));

            services.AddSingleton(new Appsettings(Configuration));
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