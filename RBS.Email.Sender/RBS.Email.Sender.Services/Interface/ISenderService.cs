using RBS.Email.Sender.Common.Models;

namespace RBS.Email.Sender.Services.Interface;

public interface ISenderService
{
    void Send(EmailModel model);
}