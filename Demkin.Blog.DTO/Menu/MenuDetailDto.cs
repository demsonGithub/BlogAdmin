using System;
using System.Collections.Generic;
using System.Text;

namespace Demkin.Blog.DTO.Menu
{
    public class MenuDetailDto
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
    }
}