using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using RBS.Email.Sender.Common.Models;
using RBS.Email.Sender.Services.Interface;

namespace RBS.Email.Sender.Services;

public class SenderService : ISenderService
{
    public SenderService()
    {
    }

    public async Task Send(EmailModel model)
    {
        var email = new MimeMessage();

        email.From.Add(MailboxAddress.Parse("i.khomenko@outlook.com"));
        email.To.Add(MailboxAddress.Parse("homathebest1@gmail.com"));
        email.Subject = "Test Email Subject";
        email.Body = new TextPart(TextFormat.Html) { Text = "<h1>Hi Bogdan</h1>" };

        // Send email.
        using var smtp = new SmtpClient();
        smtp.Connect("smtp.live.com", 587, SecureSocketOptions.StartTls);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}