using GymProject.Contract.Dtos.Auth;
using GymProject.Core.Identity.Tokens;
using GymProject.Core.UserExceptions.Identity;
using GymProject.Domain.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using GymProject.Contract.Commands.Auth;

namespace GymProject.Core.Commands.Auth;

internal sealed class SignInCommandHandler : IRequestHandler<SignInCommand, TokenDTO>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public SignInCommandHandler(
        UserManager<AppUser> userManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<TokenDTO> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        await UserValidation(request.Email, request.Password);
        var token = await _tokenService.CreateToken(user);
        return new()
        {
            Token = token.Token,
            RefreshToken = token.RefreshToken
        };
    }

    private async Task UserValidation(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null
            || !await _userManager.CheckPasswordAsync(user, password)
            || !await _userManager.IsEmailConfirmedAsync(user))
            throw new InvalidUserCredentialException();
    }

    private async Task UpdateUserRefreshToken(AppUser user, string refreshToken)
    {
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddHours(1);
        await _userManager.UpdateAsync(user);
    }
}