using Autofac;
using Autofac.Extras.DynamicProxy;
using Demkin.Blog.IService.Base;
using Demkin.Blog.Repository.Base;
using Demkin.Blog.Service.Base;
using Demkin.Blog.Utils.SystemConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Demkin.Blog.Extensions.ServiceExtensions
{
    public class AutofacModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            string basePath = AppContext.BaseDirectory;

            var serviceDllFile = Path.Combine(basePath, SiteInfo.ServiceDllName);
            var repositoryDllFile = Path.Combine(basePath, SiteInfo.RepositoryDllName);

            if (!File.Exists(serviceDllFile) || !File.Exists(repositoryDllFile))
            {
                var msg = "Repository.dll或Service.dll 丢失。";
                throw new Exception(msg);
            }

            var cacheType = new List<Type>();

            builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>)).InstancePerDependency();

            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();

            // Service注册
            var assemblyServices = Assembly.LoadFrom(serviceDllFile);

            builder.RegisterAssemblyTypes(assemblyServices)
                .AsImplementedInterfaces()
                .InstancePerDependency()
               .EnableInterfaceInterceptors()    //引用Autofac.Extras.DynamicProxy;
                      .InterceptedBy(cacheType.ToArray());//允许将拦截器服务的列表分配给注册。

            // 获取 Repository.dll 程序集服务，并注册
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository)
                   .AsImplementedInterfaces()
                   .InstancePerDependency();
        }
    }
}