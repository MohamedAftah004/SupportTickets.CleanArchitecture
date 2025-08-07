using MediatR;
using SupportTicket.Application.Features.Tickets.Admin.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.Admin.Queries.GetTicketsByStatus
{
    public record GetTicketsByStatusQuery(short Status) : IRequest<List<TicketDto>>;
    
}
