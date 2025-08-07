using MediatR;
using SupportTicket.Domain.Entities;
using SupportTicket.Domain.Interfaces.Repositories;
using SupportTicket.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        //interfaces
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
        
            //is exsist
            var exsistingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (exsistingUser is not null)
                throw new InvalidOperationException("Existing Email, Select another one");


            //hashing password
            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = hashedPassword,
                Role = request.Role,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            //adding on database
           await _userRepository.AddAsync(user);

            return user.Id;
        }
    }
}
