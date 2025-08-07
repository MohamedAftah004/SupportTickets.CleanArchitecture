using MediatR;
using SupportTicket.Application.Features.AdminControl.DTOs;
using SupportTicket.Domain.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.AdminControl.Queries.GetAllUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(user => new UserDto
            {
                Name = user.Name,
                Email = user.Email,
                Role = (user.Role == 0) ? "User" : "Admin" ,
                IsActive = user.IsActive ? "Active" : "Not-Active",
                CreatedAt = user.CreatedAt
            }).ToList();
        }
    }
    
}
