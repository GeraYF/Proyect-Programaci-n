using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Trabajo_Final.Helper
{
    public class SendMailSendGrid
    {
        private readonly string _apiKey = Environment.GetEnvironmentVariable("SEND_GRID_KEY"); // Reemplaza con tu API Key

        public async Task EnviarCorreoAsync(string emailTo, string subject, string body)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("jmguticasani1254@gmail.com", "Infocom Technology Soporte");
            var to = new EmailAddress(emailTo);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine($"Correo enviado con estado: {response.StatusCode}");
        }
    }
}