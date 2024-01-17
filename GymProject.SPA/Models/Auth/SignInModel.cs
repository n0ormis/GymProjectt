using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymProject.SPA.Models.Auth;

public class SignInModel
{
    [EmailAddress, Required]
    public string Email { get; set; }
    [Required, PasswordPropertyText]
    public string Password { get; set; }
}