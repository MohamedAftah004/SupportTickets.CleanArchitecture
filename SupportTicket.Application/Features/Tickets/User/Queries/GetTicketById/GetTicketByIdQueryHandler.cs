using MediatR;
using SupportTicket.Application.Features.Tickets.User.DTOs;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.User.Queries.GetTicketById
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto>
    {

        private readonly ITicketRepository _ticketRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetTicketByIdQueryHandler(ITicketRepository ticketRepository, ICurrentUserService currentUserService)
        {
            _ticketRepository = ticketRepository;
            _currentUserService = currentUserService;
        }

        public async Task<TicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByIdAsync(request.Id);

            if (ticket == null)
                throw new Exception("No Tickets with this Id");

            if (ticket.CreatedByUserId != _currentUserService.UserId)
                throw new UnauthorizedAccessException("You are not authorized to view this ticket.");


            return new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                CreatedAt = ticket.CreatedAt,
                Status = ticket.Status.ToString(),
                UpdatedAt = ticket.UpdatedAt,
            };

        }
    }
}
