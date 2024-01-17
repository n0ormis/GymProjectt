using GymProject.Core.Helpers.Emails;
using GymProject.Core.Services.Payments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymProject.Core.Services;


public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddHttpContextAccessor();
        return services;
    }
}