using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.Modularity
{
    public class OnApplicationInitializationModuleLifecycleContributor:ModuleLifecycleContributorBase
    {
        public override void Initialize(ApplicationInitializationContext context, IAbpModule module)
        {
            // 使用模块实例转换为 IOnApplicationInitialization 对象，调用其生命周期方法。
            (module as IOnApplicationInitialization)?.OnApplicationInitialization(context);
        }
    }

    public class OnApplicationShutdownModuleLifecycleContributor : ModuleLifecycleContributorBase
    {
        public override void Shutdown(ApplicationShutdownContext context, IAbpModule module)
        {
            (module as IOnApplicationShutdown)?.OnApplicationShutdown(context);
        }
    }

    public class OnPreApplicationInitializationModuleLifecycleContributor : ModuleLifecycleContributorBase
    {
        public override void Initialize(ApplicationInitializationContext context, IAbpModule module)
        {
            (module as IOnPreApplicationInitialization)?.OnPreApplicationInitialization(context);
        }
    }

    public class OnPostApplicationInitializationModuleLifecycleContributor : ModuleLifecycleContributorBase
    {
        public override void Initialize(ApplicationInitializationContext context, IAbpModule module)
        {
            (module as IOnPostApplicationInitialization)?.OnPostApplicationInitialization(context);
        }
    }
}
