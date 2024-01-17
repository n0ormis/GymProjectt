namespace GymProject.Core.Helpers.Emails;

internal interface IEmailService
{
    string GenerateConfirmEmail(string token, string email);
}