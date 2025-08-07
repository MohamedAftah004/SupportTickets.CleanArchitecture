using MediatR;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Enums;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.Admin.Commands.ChangeTicketStatus
{
    public class ChangeTicketStatusCommandHandler : IRequestHandler<ChangeTicketStatusCommand, bool>
    {


        private readonly ITicketRepository _ticketRepository;
        private readonly ICurrentUserService _currentUserService;

        public ChangeTicketStatusCommandHandler(ITicketRepository ticketRepository, ICurrentUserService currentUserService)
        {
            _ticketRepository = ticketRepository;
            _currentUserService = currentUserService;
        }


        public async Task<bool> Handle(ChangeTicketStatusCommand request, CancellationToken cancellationToken)
        {

            if (_currentUserService.Role != UserRole.Admin.ToString())
                throw new UnauthorizedAccessException("Access Denied");

            var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);

            if (ticket == null)
                throw new Exception("Ticket not found");

            if (!Enum.IsDefined(typeof(TicketStatus), request.StatusId))
                throw new ArgumentOutOfRangeException(nameof(request.StatusId), "Invalid ticket status.");


            ticket.AssignedByUserId = _currentUserService.UserId;
            ticket.Status = (TicketStatus)request.StatusId;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _ticketRepository.UpdateAsync(ticket);
            return true;


        }
    }
}
