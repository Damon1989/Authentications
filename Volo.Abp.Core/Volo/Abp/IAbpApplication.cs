using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Volo.Abp
{
    public interface IAbpApplication:IModuleContainer,IDisposable
    {
        Type StartupModuleType { get; }

        IServiceCollection Services { get; }
        IServiceProvider ServiceProvider { get; }
        void Shutdown();
    }
}
