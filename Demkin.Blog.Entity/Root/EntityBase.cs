using Demkin.Blog.Utils.IdGenerate;
using SqlSugar;

namespace Demkin.Blog.Entity.Root
{
    public class EntityBase
    {
        public EntityBase()
        {
            Id = IdGenerateHelper.Instance.GenerateId();
        }

        /// <summary>
        /// ID主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public long Id { get; private set; }
    }
}