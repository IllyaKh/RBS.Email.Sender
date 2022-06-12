using RBS.Email.Sender.Common.Models;

namespace RBS.Email.Sender.Services.Interface
{
    public interface IEmailDataService
    {
        Task AddEmailToHistory(EmailModel model, bool isSuccess);
    }
}
