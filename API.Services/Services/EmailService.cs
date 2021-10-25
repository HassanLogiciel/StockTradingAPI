using API.Common;
using API.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services
{
    public class EmailService : IEmailService
    {
        public async Task<Response> SendEmail(string sendTo, string sendFrom, string body,string subject)
        {
            var response = new Response();
            try
            {
                using (var smtpClient = new SmtpClient())
                using (var message = new MailMessage())
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential("", "");
                    // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;
                    message.To.Add(sendTo);
                    message.Subject = subject;
                    message.Body = body;
                    message.From = new MailAddress("Admin@stockTrader.com", "Stock Trader");
                    await smtpClient.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message.ToString());
            }
            return response;
        }
    }
}
