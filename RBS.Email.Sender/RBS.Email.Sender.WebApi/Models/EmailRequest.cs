namespace RBS.Email.Sender.WebApi.Models
{
    public class EmailRequest
    {
		public List<string> ToAddresses { get; set; }
		public List<string> FromAddresses { get; set; }
		public List<string> CcAddresses { get; set; }

		public byte[] Attachment { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }

		public bool IsHtml { get; set; }
	}
}
