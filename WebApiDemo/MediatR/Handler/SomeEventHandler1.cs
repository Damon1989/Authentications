using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WebApiDemo.MediatR.Notification;

namespace WebApiDemo.MediatR.Handler
{
    public class SomeEventHandler1:INotificationHandler<SomeEvent>
    {
        private readonly ILogger<SomeEventHandler1> _logger;

        public SomeEventHandler1(ILogger<SomeEventHandler1> logger)
        {
            _logger = logger;
        }

        public async Task Handle(SomeEvent notification, CancellationToken cancellationToken)
        {
            Task.Run(()=>
            {
                if (_logger != null) _logger.LogWarning($"Handled:{notification.Message}");
            });
        }
    }
}
