using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.User.Commands.CloseTicket
{
    public record CloseTicketCommand(Guid TicketId):IRequest<bool>;
    

}
