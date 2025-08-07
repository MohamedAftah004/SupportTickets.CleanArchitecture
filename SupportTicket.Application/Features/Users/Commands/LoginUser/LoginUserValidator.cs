using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
        }
    }
}
