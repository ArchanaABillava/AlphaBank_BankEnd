using MimeKit;
using System.Globalization;

namespace BankAuth.Models
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public Message(IEnumerable<string> to,string subject,string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("Alpha Bank",x)));
            Subject = subject;
            Content = content;
        }
    }
}
