using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp
{
    public class ApplicationInitializationContext:IServiceProviderAccessor
    {
        public IServiceProvider ServiceProvider { get; set; }

        public ApplicationInitializationContext([NotNull]IServiceProvider serviceProvider)
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));
            ServiceProvider = serviceProvider;
        }
    }
}
