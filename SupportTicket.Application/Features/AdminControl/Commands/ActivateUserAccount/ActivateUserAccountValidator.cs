using FluentValidation;
using SupportTicket.Application.Features.AdminControl.Commands.ActivateUserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.AdminControl.Commands.ActivateUserAccount
{
    public class ActivateUserAccountValidator : AbstractValidator<ActivateUserAccountCommand>
    {
        public ActivateUserAccountValidator()
        {
            RuleFor(x => x.TargetUserEmail)
            .NotEmpty().WithMessage("Target user email is required");

        }
    }
}
