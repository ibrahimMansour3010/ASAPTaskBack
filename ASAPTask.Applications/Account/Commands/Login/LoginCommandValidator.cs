using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Account.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(e => e.UserName).NotEmpty().WithErrorCode("User Name Required");

            RuleFor(e => e.Password).NotEmpty().WithErrorCode("Password Required");
        }
    }
}
