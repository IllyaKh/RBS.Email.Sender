using RBS.Email.Sender.Common.Models;

namespace RBS.Email.Sender.Services.Interface;

public interface ISenderService
{
    Task Send(EmailModel model);
}