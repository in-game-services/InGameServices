using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using InGameServices.Application.Helpers.Abstractions;
using InGameServices.Data.Entities;

namespace InGameServices.Application.Services
{
    public class MailSender : IMailSender
    {
        IConfiguration _configuration;
        public MailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMail(User user, string newUserPassword)
        {
            var ourEmail = _configuration["MailSettings:Email"];
            var ourPassword = _configuration["MailSettings:Password"];

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(ourEmail, ourPassword),
                EnableSsl = true,
            };

            smtpClient.Send(ourEmail, user.Email, "Your new In Game Services password", $"Hello {user.FirstName}, new In Game Services password is {newUserPassword}, use it to log in to the application and then change your password");
        }
    }
}



