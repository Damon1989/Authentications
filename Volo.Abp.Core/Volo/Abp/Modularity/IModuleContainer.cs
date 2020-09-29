using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Modularity;

namespace Volo.Abp.Modularity
{
    public interface IModuleContainer
    {
        [NotNull]
        IReadOnlyList<IAbpModuleDescriptor> Modules { get; }
    }
}
