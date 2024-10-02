using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EmailService.Model;
using eShopSolution.EmailService.Service;
using Microsoft.Extensions.Options;
using MimeKit;

namespace eShopSolution.EmailService.Gmail
{
    public class EmailSender : ISenderEmail
    {
        private readonly EmailConfiguration _EmailConfiguration;
        public EmailSender(IOptions<EmailConfiguration> options)
        {
            _EmailConfiguration = options.Value;
        }
        public Response<string> SendEmail(Message message)
        {
            var email = CreateEmailMessage(message);
            return send(email);

        }
        public MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("EshopTinghow", _EmailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
            return emailMessage;
        }
        public Response<string> send(MimeMessage message)
        {
            using var client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                client.Connect(_EmailConfiguration.SmtpServer, _EmailConfiguration.Port, true);
                client.Authenticate(_EmailConfiguration.Username, _EmailConfiguration.Password);
                client.Send(message);
                return new Response<string> { IsSuccess = true, Value = "Check Email" };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Response<string> { IsSuccess = false, Value = ex.ToString() };
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
