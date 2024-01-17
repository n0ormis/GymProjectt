namespace GymProject.Core.UserExceptions.Identity;

internal sealed class UserNotFoundException : CustomException
{
    public UserNotFoundException() : base("User not found") {}
}