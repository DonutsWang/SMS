using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Entity
{
    public class Query
    {
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageNum { get; set; }
        /// <summary>
        /// 附加搜索条件
        /// </summary>
        public string Where { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalNumber { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Order { get; set; }
    }
}
