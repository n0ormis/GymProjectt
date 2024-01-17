using GymProject.Core.UserExceptions.Identity;
using GymProject.Domain.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using GymProject.Contract.Commands.Auth;

namespace GymProject.Core.Commands.Auth;

internal sealed class ConfirmMailCommandHandler : IRequestHandler<ConfirmMailCommand, string>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _configurationSection;

    public ConfirmMailCommandHandler(
        UserManager<AppUser> userManager, 
        IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration; 
        _configurationSection = _configuration.GetSection("SPA");

    }

    public async Task<string> Handle(ConfirmMailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            throw new UserNotFoundException();
        var result = await _userManager.ConfirmEmailAsync(user, request.Token);
        
        if (!result.Succeeded)
            throw new InvalidTokenException();

        return _configurationSection["http"];
    }
}