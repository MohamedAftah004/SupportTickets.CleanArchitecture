using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.Admin.Commands.AssignTicket
{
    public record AssignTicketCommand(Guid ticketId) : IRequest<bool>;
}
