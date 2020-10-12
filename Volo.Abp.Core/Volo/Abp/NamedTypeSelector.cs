using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp
{
    public class NamedTypeSelector
    {
        public string Name { get; set; }
        public Func<Type,bool> Predicate { get; set; }

        public NamedTypeSelector(string name,Func<Type,bool> predicate)
        {
            Name = name;
            Predicate = predicate;
        }
    }
}
