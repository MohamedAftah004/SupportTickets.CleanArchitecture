using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.AdminControl.Commands.UpdateUserProfileByAdmin
{
    public record UpdateUserProfileCommand(string Email , string Name , bool IsActive) : IRequest<bool>;
}
