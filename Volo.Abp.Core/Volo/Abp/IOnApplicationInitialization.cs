using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Volo.Abp
{
    public interface IOnApplicationInitialization
    {
        void OnApplicationInitialization([NotNull] ApplicationInitializationContext context);
    }
}
