using MailKit.Security;
using MimeKit;
using Online_Shop.Interfaces.ServiceInterfaces;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Runtime;
using MailKit.Net.Smtp;

namespace Online_Shop.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public async Task SendEmail(string email, string verification)
        {

            string html = "<p>" + "Your verfication has been "+ verification + "</p>";
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress(_configuration["Email:DisplayName"], _configuration["Email:Address"]));
            mail.To.Add(MailboxAddress.Parse(email));

            mail.Subject = "Verification";

            var builder = new BodyBuilder();
            builder.HtmlBody = html;
            mail.Body = builder.ToMessageBody();

            SmtpClient smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration["Email:Host"], int.Parse(_configuration["Email:Port"]!), SecureSocketOptions.Auto);
            await smtp.AuthenticateAsync(_configuration["Email:Address"], _configuration["Email:Password"]);
            await smtp.SendAsync(mail);
            await smtp.DisconnectAsync(true);
        }
    }
}
