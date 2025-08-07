using MediatR;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Enums;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.User.Commands.DeleteTicket
{
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, bool>
    {

        private readonly ITicketRepository _ticketRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteTicketCommandHandler(ITicketRepository ticketRepository, ICurrentUserService currentUserService)
        {
            _ticketRepository = ticketRepository;
            _currentUserService = currentUserService;

        }

        public async Task<bool> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
            if (ticket == null)
                throw new Exception("Ticket not found");

            //is currentUser owner  ?
            if (ticket.CreatedByUserId != _currentUserService.UserId)
                throw new UnauthorizedAccessException("You are not the owner of this ticket.");

            await _ticketRepository.DeleteAsync(ticket.Id);

            return true;
        }
    }
}
