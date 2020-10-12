using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Volo.Abp
{
    /// <summary>
    ///  This exception type is directly shown to the user.
    /// </summary>
    [Serializable]
    public class UserFriendlyException:BusinessException,IUserFriendlyException
    {
        public UserFriendlyException(
            string message,
            string code = null,
            string details = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning)
            : base(
                code,
                message,
                details,
                innerException,
                logLevel)
        {
            Details = details;
        }

        public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
