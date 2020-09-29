using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Volo.Abp.Internal
{
    internal static class InternalServiceCollectionExtensions
    {
        internal static void AddCoreServices(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();
            services.AddLocalization();
        }

        internal static void AddCoreAbpServices(this IServiceCollection services,
            IAbpApplication abpApplication,
            AbpApplicationCreationOptions applicationCreationOptions)
        {
        }

    }
}
