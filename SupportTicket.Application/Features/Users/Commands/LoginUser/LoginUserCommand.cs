using MediatR;

namespace SupportTicket.Application.Features.Users.Commands.LoginUser
{
    public record LoginUserCommand(string Email , string Password):IRequest<string>;
}
