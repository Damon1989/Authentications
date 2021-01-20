using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace WebApiDemo.MediatR.Request
{
    public class CreateUserCommand:IRequest<string>
    {
        public string Name { get; set; }
    }
}
