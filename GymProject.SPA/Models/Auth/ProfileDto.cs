namespace GymProject.Contract.Dtos.Auth;

public class ProfileDto
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool Active { get; set; }
    public DateTime? ActiveTo { get; set; }
}