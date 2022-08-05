using Demkin.Blog.Utils.ClassExtension;
using Demkin.Blog.Utils.Help;

namespace Demkin.Blog.Utils.SystemConfig
{
    /// <summary>
    /// 网站基础信息相关配置
    /// </summary>
    public class SiteInfo
    {
        public static string ServiceDllName => Appsettings.GetValue("SiteInfo", "ServiceDllName") + ".dll";

        public static string RepositoryDllName => Appsettings.GetValue("SiteInfo", "RepositoryDllName") + ".dll";

        public static string DtoDllName => Appsettings.GetValue("SiteInfo", "DtoDllName");

        /// <summary>
        /// 是否开启调试SQL
        /// </summary>
        public static bool IsDebugSql => Appsettings.GetValue("SiteInfo", "IsDebugSql").ObjToBool();
    }
}