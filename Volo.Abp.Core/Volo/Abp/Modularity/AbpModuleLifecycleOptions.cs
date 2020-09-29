using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Collections;
using Volo.Abp.Core.Volo.Abp.Collections;
using Volo.Abp.Modularity;

namespace Volo.Abp.Modularity
{
    public class AbpModuleLifecycleOptions
    {
        public ITypeList<IModuleLifecycleContributor> Contributors { get; }
        public AbpModuleLifecycleOptions()
        {
            Contributors=new TypeList<IModuleLifecycleContributor>();
        }
    }
}
