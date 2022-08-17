using Demkin.Blog.Enum;

namespace Demkin.Blog.DTO.MenuPermission
{
    public class MenuPermissionInsertDto
    {
        /// <summary>
        /// 父级节点的Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public LinkType LinkType { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNumber { get; set; }
    }
}