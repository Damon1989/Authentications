using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.Reflection
{
    public interface ITypeFinder
    {
        IReadOnlyList<Type> Types { get; }
    }
}
