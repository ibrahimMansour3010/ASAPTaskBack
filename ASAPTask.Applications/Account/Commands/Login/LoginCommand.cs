using ASAPTask.Applications.Account.Commands.Login.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Account.Commands.Login
{
    public class LoginCommand:IRequest<LoginOutput>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
