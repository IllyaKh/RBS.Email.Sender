using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RBS.Email.Sender.Common.Configuration;
using RBS.Email.Sender.DataAccess.Interfaces;
using RBS.Email.Sender.DataAccess.MongoModels;

namespace RBS.Email.Sender.DataAccess.DataAccess
{
    public class SendEmailItemDataAccess : ISendEmailItemDataAccess
    {
        private readonly MongoDbOptions _mongoDbOptions;

        public SendEmailItemDataAccess(IOptions<MongoDbOptions> mongoOptions)
        {
            _mongoDbOptions = mongoOptions.Value;
        }

        public async Task AddSendRequest(SendEmailItem item)
        {
            var messagesCollection = ConnectToMongo<SendEmailItem>(_mongoDbOptions.MessagesCollectionName);
            
            await messagesCollection.InsertOneAsync(item); 
        }

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(_mongoDbOptions.ConncetionString);
            var db = client.GetDatabase(_mongoDbOptions.DbName);

            return db.GetCollection<T>(collection);
        }
    }
}
