namespace GymProject.Core.UserExceptions.Identity;

internal sealed class InvalidTokenException : CustomException
{
    public InvalidTokenException() : base("Invalid token") {}
}