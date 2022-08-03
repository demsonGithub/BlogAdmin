using Demkin.Blog.Entity.Root;
using SqlSugar;

namespace Demkin.Blog.Entity
{
    [SugarTable("Auth_Permission")]
    public class Permission : EntityRecordBase
    {
        /// <summary>
        /// 操作显示名称
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 30)]
        public string ActionName { get; set; }

        /// <summary>
        /// 路由操作地址
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 255)]
        public string ActionUrl { get; set; }

        /// <summary>
        /// 菜单模块的Id
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long MenuId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Description { get; set; }
    }
}