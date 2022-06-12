namespace RBS.Email.Sender.Common.Configuration
{
    public class MongoDbOptions
    {
        public string ConncetionString { get; set; }

        public string DbName { get; set; }

        public string MessagesCollectionName { get; set; }
    }
}
