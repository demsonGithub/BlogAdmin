using Demkin.Blog.Entity.Root;
using Demkin.Blog.Enum;
using SqlSugar;

namespace Demkin.Blog.Entity
{
    [SugarTable("Auth_MenuPermission")]
    public class MenuPermission : EntityRecordBase
    {
        /// <summary>
        /// 父级菜单的Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50)]
        public string Name { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200)]
        public string LinkUrl { get; set; }

        /// <summary>
        /// 路由操作地址
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 255)]
        public string ActionUrl { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Icon { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(IsNullable = true, Length = int.MaxValue)]
        public string Description { get; set; }

        /// <summary>
        /// 类型(目录，菜单，按钮)
        /// </summary>
        public LinkType LinkType { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int SortNumber { get; set; }
    }
}