using MediatR;
using SupportTicket.Application.Features.Users.DTOs;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Users.Queries.GetMyProfile
{
    public class GetMyProfileHandler : IRequestHandler<GetMyProfileQuery, GetMyProfileDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetMyProfileHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
        }

        public async Task<GetMyProfileDto> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {

            var userId = _currentUserService.UserId;
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found.");

            return new GetMyProfileDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsActive = user.IsActive
            };

        }
    } 
}
