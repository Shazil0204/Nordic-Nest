using System.Net;
using System.Net.Mail;


namespace EmailVerification
{
    public class EmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            Console.WriteLine($"Mail Server from EmailSender: {_configuration["EmailSettings:MailServer"]}");
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient(_configuration["EmailSettings:MailServer"], int.Parse(_configuration["EmailSettings:MailPort"]))
            {
                Credentials = new NetworkCredential(_configuration["EmailSettings:Sender"], _configuration["EmailSettings:Password"]),
                EnableSsl = true,
            };

            return client.SendMailAsync(new MailMessage(_configuration["EmailSettings:Sender"], email, subject, message)
            {
                IsBodyHtml = true
            });
        }
    }
}
