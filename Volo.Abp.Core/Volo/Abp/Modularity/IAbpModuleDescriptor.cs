using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.Modularity
{
    public interface IAbpModuleDescriptor
    {
        // 模块
        Type Type { get; }
        // 模块所在的程序集
        Assembly Assembly { get; }
        // 模块的单例实例
        IAbpModule Instance { get; }
        // 是否是一个插件
        bool IsLoadedAsPlugIn { get; }
        // 依赖的其他模块
        IReadOnlyList<IAbpModuleDescriptor> Dependencies { get; }
    }
}
