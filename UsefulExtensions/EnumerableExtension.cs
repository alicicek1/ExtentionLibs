using System;
using System.Collections.Generic;
using System.Text;

namespace UsefulExtensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> DistinctByIdWithoutYield<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            List<T> ts = new List<T>();
            HashSet<TKey> keys = new HashSet<TKey>();
            foreach (T item in source)
            {
                if (keys.Add(selector(item)))
                {
                    ts.Add(item);
                }
            }
            return ts;
        }
        public static IEnumerable<T> DistinctById<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            HashSet<TKey> keys = new HashSet<TKey>();
            foreach (T item in source)
            {
                if (keys.Add(selector(item)))
                {
                    yield return item;
                }
            }
        }
    }
}
