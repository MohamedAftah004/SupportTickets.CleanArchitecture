using MediatR;
using SupportTicket.Application.Features.Tickets.User.DTOs;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.User.Queries.GetMyTickets
{
    internal class GetMyTIcketsQueryHandler : IRequestHandler<GetMyTicketsQuery, List<TicketDto>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetMyTIcketsQueryHandler(ITicketRepository ticketRepository, ICurrentUserService currentUserService)
        {
            _ticketRepository = ticketRepository;
            _currentUserService = currentUserService;
        }
        public async Task<List<TicketDto>> Handle(GetMyTicketsQuery request, CancellationToken cancellationToken)
        {

            var ticketList = await _ticketRepository.GetByUserIdAsync(_currentUserService.UserId);


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
