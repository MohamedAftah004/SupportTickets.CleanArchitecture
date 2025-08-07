using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.User.Commands.UpdateTicket
{
    public record UpdateTicketCommand(Guid TicketId , string Title , string Description) : IRequest<bool>;
}
