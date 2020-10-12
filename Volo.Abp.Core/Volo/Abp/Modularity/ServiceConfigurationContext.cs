using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Volo.Abp.Modularity
{
    public class ServiceConfigurationContext
    {
        public IServiceCollection Services { get; }
        public IDictionary<string,object> Items { get; set; }

        /// <summary>
        /// Gets/sets arbitrary named objects those can be stored during
        /// the service registration phase and shared between modules
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get => Items.GetOrDefault(key);
            set => Items[key] = value;
        }

        public ServiceConfigurationContext([NotNull]IServiceCollection services)
        {
            Services = Check.NotNull(services, nameof(services));
            Items=new Dictionary<string, object>();
        }
    }
}
