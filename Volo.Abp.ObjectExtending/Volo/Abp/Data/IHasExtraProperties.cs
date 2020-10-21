using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.Data
{
    public interface IHasExtraProperties
    {
        Dictionary<string,object> ExtraProperties { get; }
    }
}
