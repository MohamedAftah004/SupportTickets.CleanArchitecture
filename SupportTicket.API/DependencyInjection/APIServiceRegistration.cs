using Microsoft.Extensions.DependencyInjection;
using SupportTicket.API.Services;
using SupportTicket.Application.Interfaces.Services;

namespace SupportTicket.API.DependencyInjection
{
    public static class APIServiceRegistration
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            // Inject CurrentUserService علشان تعرف مين اللي عامل الطلب
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            // تديك الوصول للـ HttpContext جوا أي Service
            services.AddHttpContextAccessor();

            // Swagger - لتوثيق الـ APIs
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
