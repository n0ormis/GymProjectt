using Microsoft.AspNetCore.Identity;

namespace GymProject.Domain.Models.Auth;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; }
    public bool Paid { get; set; }
    public DateTime? MembershipActiveTo { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}