using MediatR;


namespace SupportTicket.Application.Features.Users.Commands.UpdateUserProfile
{
    public record UpdateUserProfileCommand(Guid UserId , string Name , bool IsActive) : IRequest<bool>;

}
