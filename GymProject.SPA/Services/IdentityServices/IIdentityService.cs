using GymProject.SPA.Models.Auth;

namespace ThePowerSPAv2.Services.IdentityService;

public interface IIdentityService
{
    Task<bool> SignUp(SignUpModel model);
    Task<bool> SignIn(SignInModel model);

}