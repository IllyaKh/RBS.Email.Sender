using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RBS.Email.Sender.Common.Models;
using RBS.Enums.Email;

namespace RBS.Email.Sender.DataAccess.MongoModels
{
    public class SendEmailItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Subject { get; set; }

        public EmailType EmailType { get; set; }

        public string? ToEmail { get; set; }

        public string? CcEmail { get; set; }

        public string? Content { get; set; }

        public bool IsHtml { get; set; }

        public bool IsSuccess { get; set; }

        public SendEmailItem(EmailModel model, bool isSuccess)
        {
            Subject = model.Subject;
            EmailType = EmailType.RegistrationConfirm;
            ToEmail = model?.ToAddresses?.FirstOrDefault(); 
            CcEmail = model?.CcAddresses.FirstOrDefault();
            Content = model?.Content;
            IsHtml = model.IsHtml;
            IsSuccess = isSuccess;
        }
    }
}
