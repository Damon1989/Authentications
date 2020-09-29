using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.Modularity
{
    public interface IPostConfigureServices
    {
        void PostConfigureServices(ServiceConfigurationContext context);
    }
}
