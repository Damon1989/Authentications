using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class AbpObjectExtensions
    {
        public static T As<T>(this object obj)
            where T:class
        {
            return (T) obj;
        }

        public static T To<T>(this object obj)
            where T : struct
        {
            return (T) Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }

        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }

        public static T If<T>(this T obj, bool condition, Func<T, T> func)
        {
            if (condition)
            {
                return func(obj);
            }

            return obj;
        }

        public static T If<T>(this T obj, bool condition, Action<T> action)
        {
            if (condition)
            {
                action(obj);
            }

            return obj; 
        }
    }
}
