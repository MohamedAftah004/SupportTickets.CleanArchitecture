using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Domain.Enums;
using System.Security.Claims;

namespace SupportTicket.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId => Guid.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue("uid"),out var userId)
            ?userId : Guid.Empty;

        public string Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

        public string Role => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role) ?? "User";
    }
}
