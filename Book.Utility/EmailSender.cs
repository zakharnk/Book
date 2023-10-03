using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Utility
{
    public class EmailSender : IEmailSender
    {
        public string SendGridSecretKey { get; set; }
        public EmailSender(IConfiguration _configuration)
        {
            SendGridSecretKey = _configuration.GetValue<string>("SendGrid:SecretKey");
                }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //TODO: add logic
            var client = new SendGridClient(SendGridSecretKey);

            var from = new EmailAddress("antonbohdan@rovensys.com", "Book Store");
            var to = new EmailAddress(email);
            var message = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            return client.SendEmailAsync(message);
        }
    }
}
