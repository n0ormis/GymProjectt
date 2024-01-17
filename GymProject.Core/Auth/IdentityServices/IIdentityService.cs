using System.Security.Claims;
using GymProject.Contract.Dtos.Auth;
using GymProject.Domain.Models.Auth;

namespace GymProject.Core.Auth;

public interface IIdentityService
{
    Task VerifyPayment(Guid userId, int duration);
    string GetUserId();
    Task<List<Claim>> GetClaims(AppUser user);
    Task SetRefreshTokens(AppUser user, string refreshToken);
    Task<ProfileDto> GetCurrentUser();
}