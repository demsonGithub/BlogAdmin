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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            services.AddControllers();

            services.AddSingleton(new Appsettings(Configuration));

            services.AddSwaggerSetup();

            // SqlSugar
            services.AddSqlSugarSetup();
            services.AddScoped<MyDbContext>();

            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
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