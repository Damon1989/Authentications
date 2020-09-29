using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Modularity
{
    public interface IModuleLifecycleContributor:ITransientDependency
    {
        void Initialize([NotNull] ApplicationInitializationContext context, [NotNull] IAbpModule module);
        void Shutdown([NotNull] ApplicationShutdownContext context, [NotNull] IAbpModule module);
    }
}
