using System;
using System.Collections.Generic;

namespace Demkin.Blog.DTO
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class PageModel<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// 每页数据大小
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 总数
        /// </summary>
        public int PageTotal { get; set; } = 0;

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount => (int)Math.Ceiling((decimal)PageCount / PageSize);

        /// <summary>
        /// 返回的数据
        /// </summary>
        public List<T> Data { get; set; }
    }
}