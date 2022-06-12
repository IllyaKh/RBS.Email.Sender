using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RBS.Email.Sender.Common.Configuration;
using RBS.Email.Sender.DataAccess.Interfaces;
using RBS.Email.Sender.DataAccess.MongoModels;

namespace RBS.Email.Sender.DataAccess.DataAccess
{
    public class SendEmailItemDataAccess : ISendEmailItemDataAccess
    {
        private readonly ILogger<SendEmailItemDataAccess> _logger;
        private readonly MongoDbOptions _mongoDbOptions;

        public SendEmailItemDataAccess(IOptions<MongoDbOptions> mongoOptions, 
            ILogger<SendEmailItemDataAccess> logger)
        {
            _mongoDbOptions = mongoOptions.Value;
            _logger = logger;
        }

        public async Task AddSendRequest(SendEmailItem item)
        {
            try
            {
                _logger.LogWarning("Adding to mongo model.", item);

                var messagesCollection = ConnectToMongo<SendEmailItem>(_mongoDbOptions.MessagesCollectionName);

                await messagesCollection.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError("En error occured while saving send result to Mongo. Ex:", ex);
                throw;
            }

        }

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(_mongoDbOptions.ConncetionString);
            var db = client.GetDatabase(_mongoDbOptions.DbName);

            return db.GetCollection<T>(collection);
        }
    }
}
