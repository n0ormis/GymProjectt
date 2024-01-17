using GymProject.Domain.Models.Auth;
using GymProject.Domain.Models.Memberships;
using GymProject.Infastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GymProject.Infastructure.DAL;

internal static class DbIntializer
{
    public static void InitializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        var appDbContext = services.GetService<AppDbContext>();
        var userManager = services.GetService<UserManager<AppUser>>();
        var roleManager = services.GetService<RoleManager<IdentityRole>>();

        appDbContext.Database.Migrate();
        
        foreach (var role in RoleTypes.GetList())
        {
            var result = roleManager.RoleExistsAsync(role).Result;
            if (!result)
            {
                var creationResult = roleManager.CreateAsync(new IdentityRole(role)).Result;
            }
        }

        if (appDbContext.Users.Count() == 0)
        {
            var user = new AppUser()
            {
                UserName = "admin",
                Fullname = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            };

            _ = userManager.CreateAsync(user, "Admin123@").Result;
            foreach (var role in RoleTypes.GetList())
                _ = userManager.AddToRoleAsync(user, role).Result;
        }
    }
}