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
        public bool InitTables { get; set; }

        /// <summary>
        /// 待生成数据库表的实体类
        /// </summary>
        public string EntityDllName { get; set; }

        /// <summary>
        /// 是否初始化数据
        /// </summary>
        public bool InitBasicData { get; set; }

        /// <summary>
        /// 初始化数据文件存放目录
        /// </summary>
        public string InitBasicDataFolder { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataBaseType Type { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
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