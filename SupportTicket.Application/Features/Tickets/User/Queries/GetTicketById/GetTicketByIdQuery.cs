using MediatR;
using SupportTicket.Application.Features.Tickets.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.User.Queries.GetTicketById
{
    public record GetTicketByIdQuery(Guid Id) : IRequest<TicketDto>;
    
}
