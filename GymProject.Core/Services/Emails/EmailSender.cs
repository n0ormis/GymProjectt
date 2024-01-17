using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace GymProject.Core.Helpers.Emails;

internal sealed class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _emailSettings;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
        _emailSettings = _configuration.GetSection("Email");
    }

    public void SendEmail(string title, string body, string to)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(MailboxAddress.Parse(_emailSettings["mail"]));
        mimeMessage.To.Add(MailboxAddress.Parse(to));
        mimeMessage.Subject = "Test email subject";
        mimeMessage.Body = new TextPart(TextFormat.Html)
        {
            Text = body,
        };
        using var smpt = new SmtpClient();
        smpt.Connect(_emailSettings["host"], Convert.ToInt32(_emailSettings["port"]), SecureSocketOptions.StartTls);
        smpt.Authenticate(_emailSettings["mail"], _emailSettings["pass"]);
        smpt.Send(mimeMessage);
        smpt.Disconnect(true);
    }
}