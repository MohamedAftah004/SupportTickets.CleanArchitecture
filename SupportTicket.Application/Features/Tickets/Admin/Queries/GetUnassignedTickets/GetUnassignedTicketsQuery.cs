using MediatR;
using SupportTicket.Application.Features.Tickets.Admin.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.Admin.Queries.GetUnassignedTickets
{
public record GetUnassignedTicketsQuery() : IRequest<List<TicketDto>>;
}
