using GymProject.Infastructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GymProject.Infastructure.DAL;
using GymProject.Infastructure.Repositories.Memberships;
using GymProject.Infastructure.Repositories.Payments;

namespace GymProject.Infastructure;

public static class Extensions
{
    public static IServiceCollection AddInfastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDb(configuration);
        services.AddSingleton<MiddlewareExceptionHandler>();
        services.AddScoped<IMembershipRepository, MembershipRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.Services.InitializeDb();
        return app;
    }
}
