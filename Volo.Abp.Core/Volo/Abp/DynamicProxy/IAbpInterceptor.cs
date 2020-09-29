using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.DynamicProxy
{
    public interface IAbpInterceptor
    {
        // 异步方法拦截
        Task InterceptAsync(IAbpMethodInvocation invocation);
    }
}
