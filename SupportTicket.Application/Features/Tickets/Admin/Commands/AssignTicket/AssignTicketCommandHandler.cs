using MediatR;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Enums;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.Admin.Commands.AssignTicket
{
    public class AssignTicketCommandHandler : IRequestHandler<AssignTicketCommand, bool>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICurrentUserService _currentUserService;

        public AssignTicketCommandHandler(ITicketRepository ticketRepository, ICurrentUserService currentUserService)
        {
            _ticketRepository = ticketRepository;
            _currentUserService = currentUserService;
        }


        public async Task<bool> Handle(AssignTicketCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new UnauthorizedAccessException("Access Denied");

            var ticket = await _ticketRepository.GetByIdAsync(request.ticketId);

            if (ticket == null)
                throw new Exception("Ticket not found");

            if (ticket.Status == TicketStatus.Closed)
                throw new Exception("Ticket cannot be assigned, it is already closed.");

            ticket.AssignedByUserId = _currentUserService.UserId;
            ticket.Status = TicketStatus.InProgress;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _ticketRepository.UpdateAsync(ticket);
            return true;
        }
    }

}
