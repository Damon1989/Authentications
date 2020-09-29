using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.Modularity
{
    public interface IPreConfigureServices
    {
        void PreConfigureServices(ServiceConfigurationContext context);
    }
}
