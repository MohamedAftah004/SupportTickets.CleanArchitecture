using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupportTicket.Application.Settings;

namespace SupportTicket.API.DependencyInjection;

public static class JwtConfiguration
{
    public static IServiceCollection ConfigureJwtSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(
            configuration.GetSection("JwtSettings"));

        return services;
    }
}
