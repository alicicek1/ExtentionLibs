using System;
using System.Collections.Generic;
using System.Text;

namespace UsefulExtensions
{
    public static class ObjectExtension
    {
        public static T ConvertTo<T>(this object obj)
        {
            try
            {
                return (T)obj;
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
