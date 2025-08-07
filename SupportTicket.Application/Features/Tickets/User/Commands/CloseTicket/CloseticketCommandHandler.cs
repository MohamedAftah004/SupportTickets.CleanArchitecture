using MediatR;
using SupportTicket.Application.Features.Tickets.User.Commands.UpdateTicket;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Enums;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.User.Commands.CloseTicket
{
    public class CloseTicketCommandHandler : IRequestHandler<CloseTicketCommand, bool>
    {


        private readonly ITicketRepository _ticketRepository;
        private readonly ICurrentUserService _currentUserService;

        public CloseTicketCommandHandler(ITicketRepository ticketRepository, ICurrentUserService currentUserService)
        {
            _ticketRepository = ticketRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(CloseTicketCommand request, CancellationToken cancellationToken)
        {

            var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
            if (ticket == null)
                throw new Exception("Ticket not found");

            //is currentUser owner  ?
            if (ticket.CreatedByUserId != _currentUserService.UserId)
                throw new UnauthorizedAccessException("You are not the owner of this ticket.");

            //cannot update closed ticket 
            if (ticket.Status == TicketStatus.Closed)
                throw new InvalidOperationException("Ticket Already Closed.");


            ticket.Status = TicketStatus.Closed;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _ticketRepository.UpdateAsync(ticket);
            return true;

        }
    }
}
