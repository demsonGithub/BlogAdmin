using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demkin.Blog.Utils.Help
{
    /// <summary>
    /// appsettins.json 读取
    /// </summary>
    public class Appsettings
    {
        private static IConfiguration _configuration;

        public Appsettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 获取appsettins的值
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static string GetValue(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    return _configuration[string.Join(':', sections)];
                }
            }
            catch (Exception) { }

            return "";
        }

        /// <summary>
        /// 配置获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static List<T> GetValue<T>(params string[] sections)
        {
            List<T> list = new List<T>();
            // 引用 Microsoft.Extensions.Configuration.Binder 包
            _configuration.Bind(string.Join(":", sections), list);
            return list;
        }
    }
}