using System.ComponentModel;

namespace Demkin.Blog.Enum
{
    public enum LinkType
    {
        /// <summary>
        /// 目录
        /// </summary>
        [Description("目录")]
        Catalog,

        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        Menu,

        /// <summary>
        /// 按钮或链接
        /// </summary>
        [Description("按钮或链接")]
        Button
    }
}