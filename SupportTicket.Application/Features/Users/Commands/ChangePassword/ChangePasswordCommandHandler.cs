using MediatR;
using SupportTicket.Domain.Interfaces.Repositories;
using SupportTicket.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public ChangePasswordCommandHandler(IUserRepository userRepository , IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return false;

            var isOldPasswordCorrect = _passwordHasher.VerifyPassword(request.CurrentPassword, user.PasswordHash);
            if (!isOldPasswordCorrect)
                return false;

            var newHashedPassword = _passwordHasher.HashPassword(request.NewPassword);
            user.PasswordHash = newHashedPassword;

            await _userRepository.UpdateAsync(user);
            return true;

        }
    }
}
