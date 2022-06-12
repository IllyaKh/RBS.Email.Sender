using RBS.Email.Sender.DataAccess.MongoModels;

namespace RBS.Email.Sender.DataAccess.Interfaces
{
    public interface ISendEmailItemDataAccess
    {
        Task AddSendRequest(SendEmailItem item);
    }
}
