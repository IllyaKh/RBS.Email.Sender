using RBS.Email.Sender.Common.Models;
using RBS.Email.Sender.DataAccess.Interfaces;
using RBS.Email.Sender.DataAccess.MongoModels;
using RBS.Email.Sender.Services.Interface;

namespace RBS.Email.Sender.Services
{
    public class EmailDataService : IEmailDataService
    {
        private readonly ISendEmailItemDataAccess _sendEmailItemDataAccess;

        public EmailDataService(ISendEmailItemDataAccess sendEmailItemDataAccess)
        {
            _sendEmailItemDataAccess = sendEmailItemDataAccess;
        }

        public async Task AddEmailToHistory(EmailModel model, bool isSuccess)
        {
            var emailItem = new SendEmailItem(model, isSuccess);

            await _sendEmailItemDataAccess.AddSendRequest(emailItem);
        }
    }
}
