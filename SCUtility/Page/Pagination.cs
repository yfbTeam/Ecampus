using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCUtility
{
    /// <summary>
    /// 泛型类
    /// </summary>
    /// <typeparam name="T">要转化的数据</typeparam>
     [Serializable]
    public class Pagination<T>
    {
        /// <summary>
        /// 记录总数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 页面尺寸
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 要显示的页数
        /// </summary>
        public int pageNumber { get; set; }
        /// <summary>
        /// 要显示的数据
        /// </summary>
        public List<T> rows { get; set; }

    }
}
