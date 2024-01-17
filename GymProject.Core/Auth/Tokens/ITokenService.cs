using System.Security.Claims;
using GymProject.Domain.Models.Auth;

namespace GymProject.Core.Identity.Tokens;

internal interface ITokenService
{
    Task<TokensModel> CreateToken(AppUser user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    void ValidateRefreshToken(AppUser user, string refreshToken);
}