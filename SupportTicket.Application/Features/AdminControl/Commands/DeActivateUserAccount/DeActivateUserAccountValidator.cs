using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.AdminControl.Commands.DeActivateUserAccount
{
    public class DeActivateUserAccountValidator : AbstractValidator<DeActivateUserAccountCommand>
    {
        public DeActivateUserAccountValidator()
        {
            RuleFor(x => x.TargetUserEmail)
            .NotEmpty().WithMessage("Target user email is required");

        }
    }
}
