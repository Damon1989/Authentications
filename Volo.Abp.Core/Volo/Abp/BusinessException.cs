using System;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Logging;

namespace Volo.Abp
{
    [Serializable]
    public class BusinessException : Exception,
        IBusinessException,
        IHasErrorCode,
        IHasErrorDetails,
        IHasLogLevel
    {
        public BusinessException(
            string code = null,
            string message = null,
            string details = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning)
            : base(message, innerException)
        {
            Code = code;
            Details = details;
            LogLevel = logLevel;
        }

        public BusinessException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        public string Code { get; set; }
        public string Details { get; set; }
        public LogLevel LogLevel { get; set; }

        public BusinessException WithData(string name, object value)
        {
            Data[name] = value;
            return this;
        }
    }
}