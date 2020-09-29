using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp
{
    public class AbpShutdownException : AbpException
    {
        public AbpShutdownException()
        {

        }

        public AbpShutdownException(string message)
            : base(message)
        {

        }

        public AbpShutdownException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public AbpShutdownException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
