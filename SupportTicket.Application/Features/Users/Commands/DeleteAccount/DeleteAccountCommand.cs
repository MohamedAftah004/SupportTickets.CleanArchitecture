using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Users.Commands.DeleteAccount
{
    public record DeleteAccountCommand(Guid UserId) : IRequest<bool>;
    
}
