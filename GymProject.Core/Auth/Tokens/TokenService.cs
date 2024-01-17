using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GymProject.Core.Auth;
using GymProject.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GymProject.Core.Identity.Tokens;

internal sealed class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _jwtSettings;
    private readonly UserManager<AppUser> _userManager;
    private readonly IIdentityService _identityService;

    public TokenService(IConfiguration configuration, 
        UserManager<AppUser> userManager, 
        IIdentityService identityService)
    {
        _configuration = configuration;
        _jwtSettings = _configuration.GetSection("JwtSettings");
        _userManager = userManager;
        _identityService = identityService;
    }

    public async Task<TokensModel> CreateToken(AppUser user)
    {
        var signingCredetials = GetSigningCredentials();
        var claims = await _identityService.GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredetials, claims);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        var refreshToken = CreateRefreshToken();
        await _identityService.SetRefreshTokens(user, refreshToken);
        return new()
        {
            Token = token,
            RefreshToken = refreshToken,
        };
    }

    private string CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = GetTokenValidationParametrs();
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    public void ValidateRefreshToken(AppUser user, string refreshToken)
    {
        if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new Exception("Invalid client request"); //TODO 
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtSettings["securityKey"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            issuer: _jwtSettings["validIssuer"],
            audience: _jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
            signingCredentials: signingCredentials);
        return tokenOptions;
    }

    private TokenValidationParameters GetTokenValidationParametrs()
        => new ()
        {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings["securityKey"])),
        ValidateLifetime = false,
        ValidIssuer = _jwtSettings["validIssuer"],
        ValidAudience = _jwtSettings["validAudience"],
    };
}