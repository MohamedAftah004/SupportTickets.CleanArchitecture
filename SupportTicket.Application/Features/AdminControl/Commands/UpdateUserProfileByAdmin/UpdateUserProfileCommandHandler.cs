using MediatR;
using SupportTicket.Domain.Entities;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.AdminControl.Commands.UpdateUserProfileByAdmin
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserProfileCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {

            var existUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existUser == null)
                throw new InvalidOperationException("User not found");

            existUser.Name = request.Name;
            existUser.IsActive = request.IsActive;

            await _userRepository.UpdateAsync(existUser);

            return true;


        }
    }
}
