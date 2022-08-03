using Demkin.Blog.Entity.Root;
using SqlSugar;

namespace Demkin.Blog.Entity
{
    [SugarTable("Auth_Role")]
    public class Role : EntityRecordBase
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50)]
        public string RoleName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 255)]
        public string Description { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNumber { get; set; }
    }
}