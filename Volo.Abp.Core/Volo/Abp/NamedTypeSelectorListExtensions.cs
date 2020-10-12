using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp
{
    public static class NamedTypeSelectorListExtensions
    {
        public static void Add(this IList<NamedTypeSelector> list, string name, params Type[] types)
        {
            Check.NotNull(list, nameof(list));
            Check.NotNull(name, nameof(name));
            Check.NotNull(types, nameof(types));

            list.Add(new NamedTypeSelector(name,type=>types.Any(type.IsAssignableFrom)));
        }
    }
}
