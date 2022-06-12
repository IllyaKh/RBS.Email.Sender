using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<SenderService> _logger;

    public SenderService(IOptions<EmailOptions> options, 
        IEmailDataService emailDataService,
        ILogger<SenderService> logger)
    {
        _emailOptions = options.Value;
        _emailDataService = emailDataService;
        _logger = logger;
    }

    public async Task Send(EmailModel model)
    {
        var isSuccess = false;
        try
        {
            _logger.LogError("Test 1");
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
            _logger.LogError("Test2");
            throw;
        }
        finally
        {
            _logger.LogError("Test3");
            await _emailDataService.AddEmailToHistory(model, isSuccess);
        }

    }

}