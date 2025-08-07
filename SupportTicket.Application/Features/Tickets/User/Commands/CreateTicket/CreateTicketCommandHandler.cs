using MediatR;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Entities;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.User.Commands.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Guid>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICurrentUserService _currentUser;

        public CreateTicketCommandHandler(ITicketRepository ticketRepository, ICurrentUserService currentUser)

        {
            _ticketRepository = ticketRepository;
            _currentUser = currentUser;
        }

        public async Task<Guid> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {


            var ticket = new Ticket
            {
                Title = request.Title,
                Description = request.Description,
                CreatedByUserId = _currentUser.UserId,
                Status = Domain.Enums.TicketStatus.New,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _ticketRepository.AddAsync(ticket);
            return ticket.Id;

        }
    }
}
