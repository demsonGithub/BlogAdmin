﻿using Demkin.Blog.Extensions.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demkin.Blog.Extensions.ServiceExtensions
{
    /// <summary>
    /// Automapper 启动服务
    /// </summary>
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();
        }
    }
}