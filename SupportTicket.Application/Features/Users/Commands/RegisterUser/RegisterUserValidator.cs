using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {

        public RegisterUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("الاسم مطلوب")
                .MaximumLength(100).WithMessage("الاسم طويل جدًا");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("الإيميل مطلوب")
                .EmailAddress().WithMessage("الإيميل غير صالح");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("كلمة السر مطلوبة")
                .MinimumLength(6).WithMessage("كلمة السر قصيرة جدًا");
        }
    }
}
