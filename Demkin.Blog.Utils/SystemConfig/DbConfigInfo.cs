using Demkin.Blog.Utils.ClassExtension;
using Demkin.Blog.Utils.Help;

namespace Demkin.Blog.Utils.SystemConfig
{
    /// <summary>
    /// 数据库相关配置文件
    /// </summary>
    public class DbConfigInfo
    {
        /// <summary>
        /// 是否初始化表
        /// </summary>
        public static bool InitTables => Appsettings.GetValue(new string[] { "DbConfigInfo", "InitTables" }).ObjToBool();

        /// <summary>
        /// 是否初始化数据
        /// </summary>
        public static bool InitBasicData => Appsettings.GetValue(new string[] { "DbConfigInfo", "InitBasicData" }).ObjToBool();

        /// <summary>
        /// 初始化数据文件存放目录
        /// </summary>
        public static string InitBasicDataFolder => Appsettings.GetValue(new string[] { "DbConfigInfo", "InitBasicDataFolder" });

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString => Appsettings.GetValue(new string[] { "DbConfigInfo", "Connection" });

        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DataBaseType Type => (DataBaseType)Appsettings.GetValue(new string[] { "DbConfigInfo", "Type" }).ObjToInt();

        /// <summary>
        /// 待生成数据库表的实体类
        /// </summary>
        public static string EntityDllName => Appsettings.GetValue("DbConfigInfo", "EntityDllName");
    }

    public enum DataBaseType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3,
        PostgreSQL = 4,
    }
}