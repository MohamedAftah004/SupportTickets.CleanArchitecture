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

namespace SupportTicket.Application.Features.TicketResponses.Queries.GetTicketResponsesByTicket
{
    public class GetTicketResponsesByTicketQueryHandler : IRequestHandler<GetTicketResponsesByTicketQuery, List<TicketResponseDto>>
    {
        private readonly ITicketResponseRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public GetTicketResponsesByTicketQueryHandler(
            ITicketResponseRepository repository,
            ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<List<TicketResponseDto>> Handle(GetTicketResponsesByTicketQuery request, CancellationToken cancellationToken)
        {
            if (!string.Equals(_currentUserService.Role, UserRole.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                throw new UnauthorizedAccessException("Only admins can view ticket responses.");

            var responses = await _repository.GetByTicketIdAsync(request.TicketId);

            return responses.Select(r => new TicketResponseDto
            {
                Id = r.Id,
                ResponseText = r.ResponseText,
                CreatedAt = r.CreatedAt,
                TicketId = r.TicketId,
                AdminUserId = r.AdminUserId,
                AdminUserName = r.AdminUser?.Name // optional
            }).ToList();
        }
    }
}
