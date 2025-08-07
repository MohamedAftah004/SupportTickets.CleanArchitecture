using MediatR;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Enums;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.User.Commands.UpdateTicket
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand , bool>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTicketCommandHandler(ITicketRepository ticketRepository, ICurrentUserService currentUserService)
        {
            _ticketRepository = ticketRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {

        var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
            if (ticket == null)
                throw new Exception("Ticket not found");

            //is currentUser owner  ?
            if (ticket.CreatedByUserId != _currentUserService.UserId)
                throw new UnauthorizedAccessException("You are not the owner of this ticket.");

            //cannot update closed ticket 
            if(ticket.Status == TicketStatus.Closed)
                throw new InvalidOperationException("You cannot update a closed ticket.");

            ticket.Title = request.Title;
            ticket.Description = request.Description;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _ticketRepository.UpdateAsync(ticket);
            return true;

        }
    }
}
