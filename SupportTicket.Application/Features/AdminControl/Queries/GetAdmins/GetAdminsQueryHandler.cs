using MediatR;
using SupportTicket.Application.Features.AdminControl.DTOs;
using SupportTicket.Application.Features.AdminControl.Queries.GetAllUsers;
using SupportTicket.Domain.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.AdminControl.Queries.GetAdmins
{
    public class GetAdminsQueryHandler : IRequestHandler<GetAdminsQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        public GetAdminsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(GetAdminsQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersAsync();
            return users.Select(user => new UserDto
            {
                Name = user.Name,
                Email = user.Email,
                Role = "Admin" ,
                IsActive = user.IsActive ? "Active" : "Not-Active",
                CreatedAt = user.CreatedAt

            }).ToList();
        }




    }
    
}
