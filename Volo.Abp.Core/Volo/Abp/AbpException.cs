using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp
{
    public class AbpException:Exception
    {
        public AbpException()
        {

        }

        public AbpException(string message)
            : base(message)
        {

        }

        public AbpException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public AbpException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
