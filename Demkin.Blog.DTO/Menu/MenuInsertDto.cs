namespace Demkin.Blog.DTO.Menu
{
    public class MenuInsertDto
    {
        /// <summary>
        /// 父级菜单的Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNumber { get; set; }
    }
}