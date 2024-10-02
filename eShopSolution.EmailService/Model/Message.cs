using MimeKit;

namespace eShopSolution.EmailService.Model
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;
        public Message(IEnumerable<string> to, string Subject, string Content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("email", x)));
            this.Subject = Subject;
            this.Content = Content;
        }


    }
}
