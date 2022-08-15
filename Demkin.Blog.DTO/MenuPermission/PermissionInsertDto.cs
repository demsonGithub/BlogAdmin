using System;
using System.Collections.Generic;
using System.Text;

namespace Demkin.Blog.DTO.MenuPermission
{
    public class PermissionInsertDto
    {
        /// <summary>
        /// 操作显示名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 路由操作地址
        /// </summary>
        public string ActionUrl { get; set; }

        /// <summary>
        /// 菜单模块的Id
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}