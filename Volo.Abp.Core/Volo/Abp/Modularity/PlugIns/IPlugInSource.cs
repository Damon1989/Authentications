using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Volo.Abp.Modularity.PlugIns
{
    public interface IPlugInSource
    {
        [NotNull]
        Type[] GetModules();
    }
}
