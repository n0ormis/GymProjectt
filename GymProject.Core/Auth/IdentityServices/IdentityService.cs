using System.Security.Claims;
using GymProject.Contract.Dtos.Auth;
using GymProject.Core.UserExceptions.Identity;
using GymProject.Domain.Models.Auth;
using GymProject.Infastructure.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GymProject.Core.Auth;

internal sealed class IdentityService : IIdentityService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(UserManager<AppUser> userManager, AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task VerifyPayment(Guid userId, int duration)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if(user is null)
            throw new UserNotFoundException();
        
        user.Paid = true;
        user.MembershipActiveTo = DateTime.UtcNow.AddDays(duration);
        await _dbContext.SaveChangesAsync();    
    }

    public string GetUserId()
    {
        var user = _httpContextAccessor.HttpContext.User;
        var isAuth = user.Identity?.IsAuthenticated is true;
        var userId = isAuth ? user.Identity.Name : "";
        if (string.IsNullOrEmpty(userId))
            throw new Exception();//TODO exception
        return userId;
    }
    
    public async Task<List<Claim>> GetClaims(AppUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Id)
        };

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }

    public async Task SetRefreshTokens(AppUser user, string refreshToken)
    {
        //TODO może zapisywać to w osobnej tablice lub coockisach lub dodać Redis 
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(1);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ProfileDto> GetCurrentUser()
    {
        var currentUser = await _userManager.FindByIdAsync(GetUserId());
        if (currentUser is null)
            throw new Exception();
        return new()
        {
            Username = currentUser.UserName,
            Name = currentUser.Fullname,
            Email = currentUser.Email,
            Active = currentUser.Paid,
            ActiveTo = currentUser.MembershipActiveTo
        };
    }
}