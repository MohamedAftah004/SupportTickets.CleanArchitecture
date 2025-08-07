using MediatR;
using SupportTicket.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Users.Commands.RegisterUser
{
public record RegisterUserCommand(string Name , string Email , string Password , UserRole Role ) : IRequest<Guid>;
}
