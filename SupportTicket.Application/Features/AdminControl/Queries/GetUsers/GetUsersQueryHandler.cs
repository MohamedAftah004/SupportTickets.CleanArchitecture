using MediatR;
using SupportTicket.Application.Features.AdminControl.DTOs;
using SupportTicket.Domain.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.AdminControl.Queries.GetUsers
{
    public class GetAdminsQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAdminsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersAsync();

            return users.Select(user => new UserDto
            {
                Name = user.Name,
                Email = user.Email,
                Role = "User",
                IsActive = user.IsActive ? "Active" : "Not-Active",
                CreatedAt = user.CreatedAt
            }).ToList();
        }

    }

}
