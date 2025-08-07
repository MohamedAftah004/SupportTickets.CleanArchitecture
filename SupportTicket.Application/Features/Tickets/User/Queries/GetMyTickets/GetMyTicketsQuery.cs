using MediatR;
using SupportTicket.Application.Features.Tickets.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SupportTicket.Application.Features.Tickets.User.Queries.GetMyTickets
{
    public record GetMyTicketsQuery: IRequest<List<TicketDto>>;
}
