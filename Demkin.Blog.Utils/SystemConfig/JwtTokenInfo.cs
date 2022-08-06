using Demkin.Blog.Utils.ClassExtension;
using Demkin.Blog.Utils.Help;

namespace Demkin.Blog.Utils.SystemConfig
{
    public class JwtTokenInfo
    {
        public static string SecretKey => Appsettings.GetValue("JwtTokenInfo", "SecretKey");
        public static string Issuer => Appsettings.GetValue("JwtTokenInfo", "Issuer");
        public static string Audience => Appsettings.GetValue("JwtTokenInfo", "Audience");
        public static double ExpiresTime => Appsettings.GetValue("JwtTokenInfo", "ExpiresTime").ObjToDouble();
    }
}