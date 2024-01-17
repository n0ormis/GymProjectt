using GymProject.Contract.Commands.Auth;
using GymProject.Contract.Dtos.Auth;
using GymProject.Core.Identity.Tokens;
using GymProject.Core.UserExceptions.Identity;
using GymProject.Domain.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymProject.Core.Commands.Auth;

internal sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenDTO>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public RefreshTokenCommandHandler(
        UserManager<AppUser> userManager, 
        ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<TokenDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty((request.RefreshToken)))
            throw new Exception();
        var user = await _userManager.Users.FirstOrDefaultAsync(prp => prp.RefreshToken == request.RefreshToken);
        if (user is null)
            throw new UserNotFoundException();
        
        _tokenService.ValidateRefreshToken(user, request.RefreshToken);
        var token = await _tokenService.CreateToken(user);
        
        return new()
        {
            Token = token.Token,
            RefreshToken = token.RefreshToken
        };
    }
}