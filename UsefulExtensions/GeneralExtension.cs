using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsefulExtensions
{
    public static class GeneralExtension
    {
        public static bool HasElement<T>(this ICollection<T> items)
        {
            return items != null && items.Count >= 0;
        }

        public static bool IsBetween<T>(this T value, T low, T high) where T : IComparable<T>
        {
            return value.CompareTo(low) >= 0 && value.CompareTo(high) <= 0;
        }

        public static void Each<T>(this ICollection<T> items, Action<T> action)
        {
            foreach (T item in items)
            {
                action(item);
            }
        }

        public static bool In<T>(this T source, params T[] list)
        {
            if (source == null)
                throw new Exception("Coming datas are wrong!");
            return list.Contains(source);
        }

        public static List<T> RemoveDuplicate<T>(this List<T> ls)
        {
            Dictionary<T, int> kV = new Dictionary<T, int>();
            List<T> ts = new List<T>();

            foreach (T item in ts)
            {
                if (!kV.ContainsKey(item))
                {
                    kV.Add(item, 0);
                    ts.Add(item);
                }
            }
            return ts;
        }
    }
}
