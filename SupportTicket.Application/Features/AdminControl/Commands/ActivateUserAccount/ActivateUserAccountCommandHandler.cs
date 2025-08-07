using MediatR;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.AdminControl.Commands.ActivateUserAccount
{
    public class ActivateUserAccountCommandHandler : IRequestHandler<ActivateUserAccountCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public ActivateUserAccountCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(ActivateUserAccountCommand request, CancellationToken cancellationToken)
        {

            var userAdmin = await _userRepository.GetByIdAsync(request.AdminId);

            if (userAdmin == null)
                throw new UnauthorizedAccessException("Admin not found");

            if (userAdmin.Role == Domain.Enums.UserRole.User)
                throw new UnauthorizedAccessException("Access denied, Only admins can change (user) status");

            var targetUser = await _userRepository.GetByEmailAsync(request.TargetUserEmail);
            if (targetUser == null) throw new UnauthorizedAccessException("User not found");

            if (userAdmin.Id == targetUser.Id)
                throw new InvalidOperationException("Admin cannot activate their own account");

            if (targetUser.IsActive)
                throw new InvalidOperationException("User is already active");

            targetUser.IsActive = true;
            await _userRepository.UpdateAsync(targetUser);

            return true;

        }
    }
}
