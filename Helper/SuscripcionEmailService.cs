using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Trabajo_Final.Helper
{
    public class SuscripcionEmailService
    {
        private readonly string _apiKey;

        public SuscripcionEmailService(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey), "La clave API de SendGrid no puede estar vacía.");

            _apiKey = apiKey;
        }

        public async Task EnviarCorreoDeSuscripcionAsync(string email, string mensaje)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("no-reply@tu-dominio.com", "Infocom Technology");
            var to = new EmailAddress(email);
            var subject = "Confirmación de Suscripción";
            var contenido = mensaje ?? "Gracias por suscribirte a nuestro boletín.";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, contenido, contenido);
            var response = await client.SendEmailAsync(msg);
            
            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception("Error al enviar el correo de suscripción.");
            }
        }
    }
}
