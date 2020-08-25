using System.Collections.Generic;

namespace Blog.Core.Model
{
    /// <summary>
    /// 通用分页信息类
    /// </summary>
    public class PageModel<T> where T : class
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; } = 5;

        /// <summary>
        /// 数据总数
        /// </summary>
        public int DataCount { get; set; } = 0;

        /// <summary>
        /// 每页数据量
        /// </summary>
        public int PageSize { get; set; } = 25;

        /// <summary>
        /// 数据
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}