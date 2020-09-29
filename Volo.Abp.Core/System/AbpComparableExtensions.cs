using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class AbpComparableExtensions
    {
        public static bool IsBetween<T>(this T value, T minInclusiveValue, T maxInclusiveValue) where T : IComparable<T>
        {
            return value.CompareTo(minInclusiveValue) >= 0 && value.CompareTo(maxInclusiveValue) <= 0;
        }
    }
}
