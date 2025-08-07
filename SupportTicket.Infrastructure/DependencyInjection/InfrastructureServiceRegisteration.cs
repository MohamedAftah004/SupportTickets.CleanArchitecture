using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;
using SupportTicket.Application.Interfaces.Services;
using SupportTicket.Application.Settings;
using SupportTicket.Domain.Interfaces.Repositories;
using SupportTicket.Infrastructure.presistence.DbContexts;
using SupportTicket.Infrastructure.presistence.Repositories;
using SupportTicket.Infrastructure.Services;


namespace SupportTicket.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // مثال على Inject لخدمة بتولد JWT Tokens
            services.Configure<JwtSettings>(options =>
                configuration.GetSection("JwtSettings").Bind(options));

            services.AddScoped<ITokenGenerator, TokenGenerator>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketResponseRepository, TicketResponseRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            // هنا ممكن كمان تضيف:
            // - Database (EF DbContext)
            services.AddDbContext<SupportTicketDbContext>(options =>
             options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // - File storage services
            // - Email sender services

            return services;
        }
    }
}
