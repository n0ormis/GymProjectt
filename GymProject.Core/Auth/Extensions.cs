using GymProject.Core.Identity.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymProject.Core.Auth;

public static class Extensions
{
    public static IServiceCollection AddAuth(this IServiceCollection service, IConfiguration configuration)
        => service.AddScoped<ITokenService, TokenService>()
            .AddScoped<IIdentityService, IdentityService>();
    
}