using MediatR;
using SupportTicket.Application.Features.TicketResponses.DTOs;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Entities;
using SupportTicket.Domain.Enums;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.TicketResponses.Commands.CreateTicketResponse
{
    public class CreateTicketResponseCommandHandler : IRequestHandler<CreateTicketResponseCommand, CreateTicketResponseDto>
    {

        private readonly ITicketResponseRepository _ticketResponseRepository;
        private readonly ICurrentUserService _currentUserService;
        public CreateTicketResponseCommandHandler(ITicketResponseRepository ticketResponseRepository, ICurrentUserService currentUserService)
        {
            _ticketResponseRepository = ticketResponseRepository;
            _currentUserService = currentUserService;
        }

        public async Task<CreateTicketResponseDto> Handle(CreateTicketResponseCommand request, CancellationToken cancellationToken)
        {
            //is user admin?
            if (!string.Equals(_currentUserService.Role, UserRole.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("Only Admins can add ticket responses.");
            }

            var response = new TicketResponse
            {
                Id = Guid.NewGuid(),
                ResponseText = request.CreateTicketResponse.ResponseText,
                TicketId = request.CreateTicketResponse.TicketId,
                AdminUserId = _currentUserService.UserId,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _ticketResponseRepository.AddAsync(response);
            return new CreateTicketResponseDto
            {
                ResponseText = response.ResponseText,
                TicketId = response.TicketId
            };

        }
    }
}
