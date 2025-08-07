using MediatR;
using SupportTicket.Application.Features.Tickets.Admin.DTOs;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.Admin.Queries.GetUnassignedTickets
{
    public class GetUnassignedTicketsQueryHandler : IRequestHandler<GetUnassignedTicketsQuery, List<TicketDto>>
    {

        private readonly ITicketRepository _ticketRepository;

        public GetUnassignedTicketsQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }



        public async Task<List<TicketDto>> Handle(GetUnassignedTicketsQuery request, CancellationToken cancellationToken)
        {
            var ticketList = await _ticketRepository.GetUnassignedTicketsAsync();

            if (ticketList == null || !ticketList.Any())
                throw new Exception("No unassigned tickets found in the system.");

            return ticketList.Select(t => new TicketDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status.ToString(),
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToList();
        }
    }
}
