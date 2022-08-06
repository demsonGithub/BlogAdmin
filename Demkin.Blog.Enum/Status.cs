using System.ComponentModel;

namespace Demkin.Blog.Enum
{
    public enum Status
    {
        /// <summary>
        /// 生效中
        /// </summary>
        [Description("生效")]
        Enable = 0,

        /// <summary>
        /// 已失效
        /// </summary>
        [Description("失效")]
        Disable = 1,//失效的还可以改为生效

        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        Deleted = 2,//软删除，已删除的无法恢复，无法看见，暂未使用
    }
}