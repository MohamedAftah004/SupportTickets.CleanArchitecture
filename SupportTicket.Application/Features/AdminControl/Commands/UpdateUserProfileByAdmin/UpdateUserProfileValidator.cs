using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.AdminControl.Commands.UpdateUserProfileByAdmin
{
    public class UpdateUserProfileValidator : AbstractValidator<UpdateUserProfileCommand>
    {
        public UpdateUserProfileValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required");

        }
    }
}
