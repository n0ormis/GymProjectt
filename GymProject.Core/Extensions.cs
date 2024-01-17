using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GymProject.Core.Auth;
using GymProject.Core.Services;

namespace GymProject.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddAuth(configuration);
        services.AddServices(configuration);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        return services;
    }
}