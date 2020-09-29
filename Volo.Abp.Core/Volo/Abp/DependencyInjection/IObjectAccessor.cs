using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Volo.Abp.DependencyInjection
{
    public interface IObjectAccessor<out T>
    {
        [CanBeNull]
        T Value { get; }
    }
}
