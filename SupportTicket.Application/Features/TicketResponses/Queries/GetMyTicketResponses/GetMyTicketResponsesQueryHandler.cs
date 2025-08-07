using MediatR;
using SupportTicket.Application.Features.TicketResponses.DTOs;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Enums;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.TicketResponses.Queries.GetMyTicketResponses
{
    public class GetMyTicketResponsesQueryHandler : IRequestHandler<GetMyTicketResponsesQuery, List<TicketResponseDto>>
    {
        private readonly ITicketResponseRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public GetMyTicketResponsesQueryHandler(
            ITicketResponseRepository repository,
            ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<List<TicketResponseDto>> Handle(GetMyTicketResponsesQuery request, CancellationToken cancellationToken)
        {
            if (!string.Equals(_currentUserService.Role, UserRole.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                throw new UnauthorizedAccessException("Only admins can view their responses.");

            var responses = await _repository.GetByUserIdAsync(_currentUserService.UserId);

            return responses.Select(r => new TicketResponseDto
            {
                Id = r.Id,
                ResponseText = r.ResponseText,
                CreatedAt = r.CreatedAt,
                TicketId = r.TicketId,
                AdminUserId = r.AdminUserId,
                AdminUserName = r.AdminUser?.Name
            }).ToList();
        }
    }
}
