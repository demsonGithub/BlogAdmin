using Demkin.Blog.Utils.Help;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Demkin.Blog.Utils.SystemConfig
{
    /// <summary>
    /// 配置信息入口
    /// </summary>
    public class ConfigSetting
    {
        private byte[] _appsettingsHash = new byte[20];

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        private static SiteInfo _siteInfo = new SiteInfo();
        private static DbConfigInfo _dbConfigInfo = new DbConfigInfo();
        private static JwtTokenInfo _jwtTokenInfo = new JwtTokenInfo();

        #region config属性

        /// <summary>
        /// 网站配置信息
        /// </summary>
        public static SiteInfo SiteInfo
        {
            get { return _siteInfo; }
        }

        /// <summary>
        /// 数据库相关信息
        /// </summary>
        public static DbConfigInfo DbConfigInfo
        {
            get { return _dbConfigInfo; }
        }

        /// <summary>
        /// JwtToken相关信息
        /// </summary>
        public static JwtTokenInfo JwtTokenInfo
        {
            get { return _jwtTokenInfo; }
        }

        #endregion config属性

        public ConfigSetting(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
            ChangeToken.OnChange(() => configuration.GetReloadToken(), () =>
            {
                ReloadConfiguration(_env);
            });

            ReloadConfiguration(_env);
        }

        public void ReloadConfiguration(IWebHostEnvironment env)
        {
            try
            {
                string fileName = env.IsProduction() ? "appsettings.json" : $"appsettings.{env.EnvironmentName}.json";

                string filePath = Path.Combine(AppContext.BaseDirectory, fileName);

                var appsettingsHash = FileHelper.ComputeHash(filePath);

                if (!_appsettingsHash.SequenceEqual(appsettingsHash))
                {
                    _configuration.GetSection(nameof(SiteInfo)).Bind(_siteInfo);
                    _configuration.GetSection(nameof(DbConfigInfo)).Bind(_dbConfigInfo);
                    _configuration.GetSection(nameof(JwtTokenInfo)).Bind(_jwtTokenInfo);

                    _appsettingsHash = appsettingsHash;
                    Console.WriteLine("配置加载完成");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "加载配置出错");
            }
        }
    }
}