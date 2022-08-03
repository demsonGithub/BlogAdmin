using System;
using System.ComponentModel;

namespace Demkin.Blog.Enum
{
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum Gender
    {
        [Description("男")]
        male = 0,

        [Description("女")]
        female = 1,

        [Description("未知")]
        unknow = 2,
    }
}