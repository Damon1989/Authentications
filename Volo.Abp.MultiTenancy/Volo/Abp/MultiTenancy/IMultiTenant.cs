using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.MultiTenancy
{
    public interface IMultiTenant
    {
        Guid? TenantId { get; }
    }
}
