using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp
{
    public sealed class NullDisposable:IDisposable
    {
        public static NullDisposable Instance { get; }=new NullDisposable();

        private NullDisposable()
        {

        }

        public void Dispose()
        {
        }
    }
}
