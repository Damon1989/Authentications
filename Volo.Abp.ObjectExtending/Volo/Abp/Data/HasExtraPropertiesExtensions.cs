using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Reflection;

namespace Volo.Abp.Data
{
    public static class HasExtraPropertiesExtensions
    {
        public static bool HasProperty(this IHasExtraProperties source, string name)
        {
            return source.ExtraProperties.ContainsKey(name);
        }

        public static object GetProperty(this IHasExtraProperties source, string name, object defaultValue = null)
        {
            return source.ExtraProperties?.GetOrDefault(name)
                   ?? defaultValue;
        }

        public static TProperty GetProperty<TProperty>(this IHasExtraProperties source, string name,
            TProperty defaultValue = default)
        {
            var value = source.GetProperty(name);
            if (value == null)
            {
                return defaultValue;
            }

            if (TypeHelper.IsPrimitiveExtended(typeof(TProperty), includeEnums: true))
            {
                return (TProperty) Convert.ChangeType(value, typeof(TProperty), CultureInfo.InvariantCulture);
            }

            throw new AbpException("GetProperty<TProperty> dose not support non-primitive types. Use non-generic GetProperty method and handle type casting manually.");
        }

        //public static TSource SetProperty<TSource>(this TSource source, string name, object value, bool validate = true)
        //    where TSource : IHasExtraProperties
        //{
        //    if (validate)
        //    {
                
        //    }
        //}
    }
}
