using Microsoft.Extensions.Configuration;

namespace Demkin.Blog.Utils.SystemConfig
{
    /// <summary>
    /// Jwt相关配置
    /// </summary>
    public class JwtTokenInfo
    {
        /// <summary>
        /// jwt密钥
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 订阅者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 有效时长（分钟）
        /// </summary>
        public double ExpiresTime { get; set; }
    }
}