using Microsoft.Extensions.Logging;

namespace Volo.Abp.Logging
{
    public interface IHasLogLevel
    {
        LogLevel LogLevel { get; set; }
    }
}