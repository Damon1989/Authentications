using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Collections;
using Volo.Abp.DynamicProxy;

namespace Volo.Abp.DependencyInjection
{
    public interface IOnServiceRegistredContext
    {
        ITypeList<IAbpInterceptor> Interceptors { get; }
        Type ImplementationType { get; }
    }
}
