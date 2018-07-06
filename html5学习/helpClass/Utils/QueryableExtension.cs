using System.Collections.Generic;
using System.Linq;

namespace helpClass.Utils
{
    public static class QueryableExtension
    {
        /// <summary>
        /// IQueryable<T>转换成List<T>,失败返回一个空的List<T>集合
        /// </summary>
        public static List<T> ToListOrDefault<T>(this IQueryable<T> source)
        {
            return source == null ? new List<T>() : source.ToList();
        }
    }
}
 