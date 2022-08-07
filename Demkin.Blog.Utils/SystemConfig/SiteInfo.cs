namespace Demkin.Blog.Utils.SystemConfig
{
    /// <summary>
    /// 网站基础信息相关配置
    /// </summary>
    public class SiteInfo
    {
        public SiteInfo()
        {
            System.Console.WriteLine("aaa");
        }

        /// <summary>
        /// 雪花算法的id
        /// </summary>
        public int SnowFlakeWorkerId { get; set; }

        private string _serviceDllName;

        /// <summary>
        /// service层的dll名称
        /// </summary>
        public string ServiceDllName
        {
            get { return _serviceDllName; }
            set { _serviceDllName = value + ".dll"; }
        }

        private string _repositoryDllName;

        /// <summary>
        /// repository层的dll名称
        /// </summary>
        public string RepositoryDllName
        {
            get { return _repositoryDllName; }
            set { _repositoryDllName = value + ".dll"; }
        }

        /// <summary>
        /// Dto的dll名称
        /// </summary>
        public string DtoDllName { get; set; }

        /// <summary>
        /// 是否开启调试SQL
        /// </summary>
        public bool IsDebugSql { get; set; }
    }
}