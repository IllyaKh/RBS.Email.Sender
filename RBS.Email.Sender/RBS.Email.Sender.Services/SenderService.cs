using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using RBS.Email.Sender.Common.Configuration;
using RBS.Email.Sender.Common.Models;
using RBS.Email.Sender.Services.Interface;

namespace RBS.Email.Sender.Services;

public class SenderService : ISenderService
{
    private readonly EmailOptions _emailOptions;
    private readonly IEmailDataService _emailDataService;

    public SenderService(IOptions<EmailOptions> options, IEmailDataService emailDataService)
    {
        _emailOptions = options.Value;
        _emailDataService = emailDataService;
    }

    public void Send(EmailModel model)
    {
        var isSuccess = false;
        try
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_emailOptions.Email));
            email.To.Add(MailboxAddress.Parse(model.ToAddresses.First()));
            email.Subject = model.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = $"<h1>{model.Content}</h1>" };

            // Send email.
            using var smtp = new SmtpClient();
            smtp.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailOptions.Email, _emailOptions.Password);
            var result = smtp.Send(email);
            smtp.Disconnect(true);
            
            isSuccess = true;
        }
        catch
        {
            throw;
        }
        finally
        {
            _emailDataService.AddEmailToHistory(model, isSuccess);
        }

    }

}