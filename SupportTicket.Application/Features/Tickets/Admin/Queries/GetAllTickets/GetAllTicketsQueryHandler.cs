using MediatR;
using SupportTicket.Application.Features.Tickets.Admin.DTOs;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.Admin.Queries.GetAllTickets
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, List<TicketDto>>
    {

        private readonly ITicketRepository _ticketRepository;

        public GetAllTicketsQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<TicketDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {

            var ticketList = await _ticketRepository.GetAllAsync();

            if (ticketList == null || !ticketList.Any())
                throw new Exception("No tickets found in the system.");

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
