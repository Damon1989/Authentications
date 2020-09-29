using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Volo.Abp.DependencyInjection
{
    public class OnServiceExposingContext: IOnServiceExposingContext
    {
        public Type ImplementationType { get; }
        public List<Type> ExposedTypes { get; }

        public OnServiceExposingContext([NotNull] Type implementationType,List<Type> exposedTypes)
        {
            ImplementationType = Check.NotNull(implementationType, nameof(implementationType));
            ExposedTypes = Check.NotNull(exposedTypes, nameof(exposedTypes));
        }
    }
}
