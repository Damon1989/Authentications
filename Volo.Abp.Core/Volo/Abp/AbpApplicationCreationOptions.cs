using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity.PlugIns;

namespace Volo.Abp
{
    public class AbpApplicationCreationOptions
    {
        public AbpApplicationCreationOptions([NotNull] IServiceCollection services)
        {
            Services = Check.NotNull(services, nameof(services));
            PlugInSources=new PlugInSourceList();
            Configuration=new AbpConfigurationiBuilderOptions();
        }

        [NotNull] public AbpConfigurationiBuilderOptions Configuration { get; }

        [NotNull] public PlugInSourceList PlugInSources { get; }

        [NotNull] public IServiceCollection Services { get; }
    }
}