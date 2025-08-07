using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.TicketResponses.Commands.DeleteTicketResponse
{
    public record DeleteTicketResponseCommand(Guid ResponseId) : IRequest<bool>;
}
