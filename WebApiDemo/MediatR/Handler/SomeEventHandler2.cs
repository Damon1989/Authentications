using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WebApiDemo.MediatR.Notification;

namespace WebApiDemo.MediatR.Handler
{
    public class SomeEventHandler2 : INotificationHandler<SomeEvent>
    {
        private readonly ILogger<SomeEventHandler2> _logger;

        public SomeEventHandler2(ILogger<SomeEventHandler2> logger)
        {
            _logger = logger;
        }

        public async Task Handle(SomeEvent notification, CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                if (_logger != null) _logger.LogWarning($"Handled:{notification.Message}");
            });
        }
    }
}