using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.DependencyInjection
{
    public interface IOnServiceExposingContext
    {
        Type ImplementationType { get; }
        List<Type> ExposedTypes { get; }
    }
}
