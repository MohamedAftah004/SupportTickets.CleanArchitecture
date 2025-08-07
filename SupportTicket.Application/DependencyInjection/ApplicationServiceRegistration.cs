using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;

namespace SupportTicket.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // تسجيل Handlers بتاعة MediatR (Commands/Queries)
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

            // تسجيل Validators (زي RegisterUserValidator, LoginValidator, ...)
            services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

            return services;
        }
    }
}
