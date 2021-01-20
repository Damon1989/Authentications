using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace WebApiDemo.MediatR.Notification
{
    public class SomeEvent:INotification
    {
        public string Message { get; }

        public SomeEvent(string message)
        {
            Message = message;
        }
    }
}
