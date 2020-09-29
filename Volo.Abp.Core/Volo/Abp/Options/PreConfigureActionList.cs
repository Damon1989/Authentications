using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.Options
{
    public class PreConfigureActionList<TOptions>:List<Action<TOptions>>
    {
        public void Configure(TOptions options)
        {
            foreach (var action in this)
            {
                action(options);
            }
        }
    }
}
