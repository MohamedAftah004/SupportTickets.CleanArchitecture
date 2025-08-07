using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Users.Commands.UpdateUserProfile
{
    public class UpdateUserProfile : AbstractValidator<UpdateUserProfileCommand>
    {
        public UpdateUserProfile()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
        }
    }
}
