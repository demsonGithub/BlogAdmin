using Demkin.Blog.Utils.ClassExtension;
using Demkin.Blog.Utils.Help;

namespace Demkin.Blog.Utils.IdGenerate
{
    /// <summary>
    ///生成数据库唯一主键:long类型
    /// </summary>
    public sealed class IdGenerateHelper
    {
        private int _snowFlakeWorkerId = Appsettings.GetValue("SiteInfo", "SnowFlakeWorkerId").ObjToInt();

        private static readonly IdGenerateHelper _instance = new IdGenerateHelper();
        private Snowflake _snowflake;

        public IdGenerateHelper()
        {
            _snowflake = new Snowflake(_snowFlakeWorkerId, 0, 0);
        }

        public static IdGenerateHelper Instance
        {
            get { return _instance; }
        }

        public long GenerateId()
        {
            return _snowflake.NextId();
        }
    }
}