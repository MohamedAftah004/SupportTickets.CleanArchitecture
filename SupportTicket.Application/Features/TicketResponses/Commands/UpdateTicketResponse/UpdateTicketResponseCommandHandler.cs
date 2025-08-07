using MediatR;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Enums;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.TicketResponses.Commands.UpdateTicketResponse
{
    public class UpdateTicketResponseCommandHandler : IRequestHandler<UpdateTicketResponseCommand, bool>
    {

        private readonly ITicketResponseRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTicketResponseCommandHandler(
            ITicketResponseRepository repository,
            ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }



        public async Task<bool> Handle(UpdateTicketResponseCommand request, CancellationToken cancellationToken)
        {
            // Check if user is admin
            if (!string.Equals(_currentUserService.Role, UserRole.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                throw new UnauthorizedAccessException("Only Admins can update ticket responses.");


            // Get the existing response
            var existing = await _repository.GetByIdAsync(request.UpdateDto.Id);
            if (existing == null || existing.IsDeleted)
                throw new KeyNotFoundException("Ticket response not found.");

            var adminUserId = _currentUserService.UserId;


            // Allow only the creator admin to edit
            if (existing.AdminUserId != adminUserId)
                throw new UnauthorizedAccessException("Cannot edit another admin's response.");

            // Update and save
            existing.ResponseText = request.UpdateDto.ResponseText;
            await _repository.UpdateAsync(existing);

            return true;
        }

    }
}
