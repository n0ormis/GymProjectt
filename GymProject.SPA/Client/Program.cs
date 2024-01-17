using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using GymProject.SPA.Features.Auth;
using GymProject.SPA.Features.Auth.Services;
using ThePowerSPAv2;
using ThePowerSPAv2.Services.IdentityService;
using ThePowerSPAv2.ServicesV2.Memberships;
using ThePowerSPAv2.ViewModels.Identity;
using ThePowerSPAv2.ViewModels.Memberships;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace ThePowerSPAv2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
#if DEBUG
            var apiUrl = "http://localhost:5078/";
#endif
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddTransient<IdentityViewModel>();
            builder.Services.AddScoped<RefreshTokenService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) }
                .EnableIntercept(sp));
            builder.Services.AddHttpClientInterceptor();
            builder.Services.AddScoped<HttpInterceptorService>();

            //ViewModels


            builder.Services.AddMudServices();
            builder.Services.AddScoped<IMembershipService, MembershipService>();
            builder.Services.AddTransient<MembershipViewModel>();

            //ViewModels

            await builder.Build().RunAsync();
        }
    }
}