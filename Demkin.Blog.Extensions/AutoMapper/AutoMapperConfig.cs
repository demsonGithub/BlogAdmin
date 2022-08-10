using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demkin.Blog.Extensions.AutoMapper
{
    /// <summary>
    /// 静态全局 AutoMapper 配置文件
    /// </summary>
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MenuProfile());
                cfg.AddProfile(new RoleProfile());
            });
        }
    }
}