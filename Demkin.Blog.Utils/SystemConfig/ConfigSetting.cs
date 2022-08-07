using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Serilog;
using System;

namespace Demkin.Blog.Utils.SystemConfig
{
    /// <summary>
    /// 配置信息入口
    /// </summary>
    public class ConfigSetting
    {
        private DateTime _lastTime;

        private readonly IConfiguration _configuration;

        private static SiteInfo _siteInfo = new SiteInfo();
        private static DbConfigInfo _dbConfigInfo = new DbConfigInfo();
        private static JwtTokenInfo _jwtTokenInfo = new JwtTokenInfo();

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

        public ConfigSetting(IConfiguration configuration)
        {
            _configuration = configuration;
            ChangeToken.OnChange(() => configuration.GetReloadToken(), () =>
            {
                DateTime dtNow = DateTime.Now;
                if ((TimeSpan)(dtNow - _lastTime) > TimeSpan.FromSeconds(3))
                {
                    ReloadConfiguration();
                }
            });

            ReloadConfiguration();
        }

        public void ReloadConfiguration()
        {
            try
            {
                _configuration.GetSection(nameof(SiteInfo)).Bind(_siteInfo);
                _configuration.GetSection(nameof(DbConfigInfo)).Bind(_dbConfigInfo);
                _configuration.GetSection(nameof(JwtTokenInfo)).Bind(_jwtTokenInfo);
                _lastTime = DateTime.Now;
                Console.WriteLine("配置加载完成");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "加载配置出错");
            }
        }
    }
}