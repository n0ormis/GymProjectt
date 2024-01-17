using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymProject.SPA.Models.Auth;

public class SignUpModel
{
    [Required]
    public string Fullname { get; set; }
    [EmailAddress, Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
}