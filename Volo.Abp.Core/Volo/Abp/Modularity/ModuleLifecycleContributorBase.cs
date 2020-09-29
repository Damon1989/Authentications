using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.Modularity
{
    public abstract class ModuleLifecycleContributorBase:IModuleLifecycleContributor
    {
        public virtual void Initialize(ApplicationInitializationContext context, IAbpModule module)
        {
        }

        public virtual void Shutdown(ApplicationShutdownContext context, IAbpModule module)
        {
        }
    }
}
