namespace GymProject.Core.Helpers.Emails;

internal interface IEmailSender
{
    void SendEmail(string title, string body, string to);
}