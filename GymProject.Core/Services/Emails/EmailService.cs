using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MimeKit;
using MimeKit.Text;

namespace GymProject.Core.Helpers.Emails;

internal sealed class EmailService : IEmailService
{
    private readonly LinkGenerator _urlHelper;
    private readonly IHttpContextAccessor _httpContext;

    public EmailService(LinkGenerator urlHelper, IHttpContextAccessor httpContext)
    {
        _urlHelper = urlHelper;
        _httpContext = httpContext;
    }

    public string GenerateConfirmEmail(string token, string email)
    {
        var text = GenerateConfirmLink(token, email);
        return
            $"<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n  <meta charset=\"UTF-8\">\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n  \n  <style>\n    body {{\n      margin: 0;\n      padding: 0;\n      font-family: Arial, sans-serif;\n      background-color: #f4f4f4;\n    }}\n\n    .container {{\n      max-width: 600px;\n      margin: 0 auto;\n      padding: 20px;\n      background-color: #ffffff;\n      border-radius: 8px;\n      box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\n    }}\n\n    .button {{\n      display: inline-block;\n      padding: 10px 20px;\n      background-color: #3498db;\n      color: #ffffff;\n      text-decoration: none;\n      font-weight: bold;\n      border-radius: 5px;\n    }}\n\n    .content {{\n      text-align: center;\n      padding: 20px 0;\n    }}\n  </style>\n</head>\n<body>\n  <div class=\"container\">\n    <div class=\"content\">\n      <h1 style=\"font-weight: bold; color: #333;\">ThePower</h1>\n      <p>Conform your email</p>\n   <a href=\"\"><a/>   <a href=\"{text}\" class=\"button\">Confirm</a>\n    </div>\n  </div>\n</body>\n</html>";
    }

    private string GenerateConfirmLink(string token, string email)
        => _urlHelper.GetUriByAction("ConfirmMail", "Identity",
            new { Token = token, Email = email }, _httpContext.HttpContext.Request.Scheme,
            _httpContext.HttpContext.Request.Host);

}