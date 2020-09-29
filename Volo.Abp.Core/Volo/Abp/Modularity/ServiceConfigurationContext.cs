using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Volo.Abp.Modularity
{
    public class ServiceConfigurationContext
    {
        public IServiceCollection Services { get; }
        public IDictionary<string,object> Items { get; set; }

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
