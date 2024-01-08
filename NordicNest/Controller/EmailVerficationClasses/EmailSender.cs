using System.Net;
using System.Net.Mail;


namespace NordicNest.Controller.EmailVerficationClasses
{
    public class EmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
			_configuration = configuration;
			Console.WriteLine($"Mail Server from EmailSender: {_configuration["EmailSettings:MailServer"]}");
            Console.WriteLine($"Mail Server from EmailSender: {_configuration["EmailSettings:MailPort"]}");
            Console.WriteLine($"Mail Server from EmailSender: {_configuration["EmailSettings:SenderName"]}");
            Console.WriteLine($"Mail Server from EmailSender: {_configuration["EmailSettings:Sender"]}");
            Console.WriteLine($"Mail Server from EmailSender: {_configuration["EmailSettings:Password"]}");
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
