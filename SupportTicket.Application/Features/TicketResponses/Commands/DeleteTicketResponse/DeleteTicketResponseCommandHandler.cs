using MediatR;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Enums;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.TicketResponses.Commands.DeleteTicketResponse
{
    public class DeleteTicketResponseCommandHandler : IRequestHandler<DeleteTicketResponseCommand, bool>
    {

        private readonly ITicketResponseRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteTicketResponseCommandHandler(
            ITicketResponseRepository repository,
            ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(DeleteTicketResponseCommand request, CancellationToken cancellationToken)
        {
            if (!string.Equals(_currentUserService.Role, UserRole.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                throw new UnauthorizedAccessException("Only admins can delete responses.");

            var response = await _repository.GetByIdAsync(request.ResponseId);

            if (response == null || response.IsDeleted)
                return false;

            if (response.AdminUserId != _currentUserService.UserId)
                throw new UnauthorizedAccessException("You cannot delete another admin's response.");

            response.IsDeleted = true;
            await _repository.UpdateAsync(response);

            return true;
        }
    }

}
