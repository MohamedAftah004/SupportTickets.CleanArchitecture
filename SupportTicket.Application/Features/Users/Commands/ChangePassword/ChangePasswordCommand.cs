using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Users.Commands.ChangePassword
{
public record ChangePasswordCommand(Guid UserId, string CurrentPassword , string NewPassword ) : IRequest<bool>;
}
