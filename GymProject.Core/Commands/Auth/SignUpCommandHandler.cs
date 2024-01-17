using GymProject.Contract.Commands.Auth;
using GymProject.Core.Helpers.Emails;
using GymProject.Domain.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace GymProject.Core.Commands.Auth;

internal sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IEmailSender _emailSender;

    public SignUpCommandHandler(
        UserManager<AppUser> userManager, 
        IEmailService emailService, 
        IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailService = emailService;
        _emailSender = emailSender;
    }

    public async Task Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        if (!request.ConfirmPassword.Equals(request.Password))
            throw new Exception();
        var user = new AppUser() { Fullname = request.Fullname, UserName = request.Email, Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.Select(e => e.Description).First());
        }
        await _userManager.AddToRoleAsync(user, "Client");
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        _emailSender.SendEmail("Accept email",_emailService.GenerateConfirmEmail(token, user.Email), user.Email);
    }
    
}