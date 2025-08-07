using MediatR;
using SupportTicket.Application.Features.Tickets.Admin.DTOs;
using SupportTicket.Domain.Enums;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.Admin.Queries.GetTicketsByStatus
{
    public class GetTicketsByStatusQueryHandler : IRequestHandler<GetTicketsByStatusQuery, List<TicketDto>>
    {

        private readonly ITicketRepository _ticketRepository;

        public GetTicketsByStatusQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<TicketDto>> Handle(GetTicketsByStatusQuery request, CancellationToken cancellationToken)
        {

            if (!Enum.IsDefined(typeof(TicketStatus), request.Status))
                throw new ArgumentOutOfRangeException(nameof(request.Status), "Invalid status value.");


            var ticketList = await _ticketRepository.GetByStatusAsync((TicketStatus)request.Status);

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
