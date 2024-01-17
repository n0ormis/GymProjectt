namespace GymProject.Core.UserExceptions.Identity;

internal sealed class InvalidUserCredentialException : CustomException
{
    public InvalidUserCredentialException() : base("Invalid user credentials") {}
}