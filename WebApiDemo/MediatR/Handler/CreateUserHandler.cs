using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebApiDemo.MediatR.Request;

namespace WebApiDemo.MediatR.Handler
{
    public class CreateUserHandler:IRequestHandler<CreateUserCommand,string>
    {
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult($"New name is {request.Name}");
        }
    }
}
