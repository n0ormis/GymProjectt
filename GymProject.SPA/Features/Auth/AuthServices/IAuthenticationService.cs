using GymProject.Contract.Dtos.Auth;
using GymProject.SPA.Models.Auth;

namespace GymProject.SPA.Features.Auth.Services;

public interface IAuthenticationService
{
    Task<bool> UserAuthenticated();
    Task<string> SignIn(SignInModel model);
    Task<string> RefreshToken();
    Task<ProfileDto> GetProfile();
    string GetCurrentToken();
}