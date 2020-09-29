using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Volo.Abp.DependencyInjection
{
    public class ObjectAccessor<T>:IObjectAccessor<T>
    {
        public T Value { get; }

        public ObjectAccessor()
        {
            
        }

        public ObjectAccessor([CanBeNull] T obj)
        {
            Value = obj;
        }
    }
}
