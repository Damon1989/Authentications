using System;
using JetBrains.Annotations;
using Volo.Abp.Modularity;

namespace Volo.Abp
{
    public interface IAbpApplicationWithExternalServiceProvider:IAbpApplication
    {
        void Initialize([NotNull] IServiceProvider serviceProvider);
    }
}
