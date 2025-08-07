using MediatR;
using System;
using SupportTicket.Domain.Interfaces.Repositories;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Enums;


namespace SupportTicket.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand , string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }


        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user is null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("الإيميل أو الباسورد غلط.");

            if (!user.IsActive)
                throw new UnauthorizedAccessException("This account has been deactivated.");

            var token = _tokenGenerator.GenerateToken(user.Id , user.Name, user.Role.ToString());
            return token;
        }
    }
}
