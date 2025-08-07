using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.User.Commands.CreateTicket
{
    public record CreateTicketCommand(string Title, string Description) : IRequest<Guid>;
}
