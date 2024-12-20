using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;
using Trabajo_Final.Models;

namespace Trabajo_Final.Helper
{
    public class SendMailSendGrid
    {
        private string _apiKey = Environment.GetEnvironmentVariable("KEYSEND");

        public async Task EnviarCorreoAsync(Contacto contacto, string msj)
        {
            // _apiKey = Environment.GetEnvironmentVariable("KEYSEND");
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("infocomtechnologysoport@gmail.com", "Infocom Technology Soporte");
            var to = new EmailAddress(contacto.Email);
            string subject = null;
            string envio = null;
            Log.Information("Preparando el mensaje para el contacto: {Nombre}", contacto.Nombre);
            if (msj.Equals("Positivo"))
            {
                subject = "¡Gracias por tu comentario!";
                envio = $"Hola {contacto.Nombre},\n¡Gracias por tomarte el tiempo para compartir tu experiencia con nosotros! Nos alegra saber que estás satisfecho con tu compra y que nuestro servicio ha cumplido tus expectativas. En INFOCOM, nos esforzamos por brindar la mejor calidad y atención a nuestros clientes.\n\nSi tienes alguna otra consulta o necesitas asistencia adicional, no dudes en ponerte en contacto con nosotros. ¡Esperamos verte de nuevo pronto!\nSaludos cordiales,\n\nEl equipo de INFOCOM Technology";
                Console.WriteLine("POSITIVO");
            }
            else
            {
                subject = "Lamentamos tu experiencia";
                envio = $"Hola {contacto.Nombre},\nLamentamos saber que tu experiencia con nosotros no ha sido satisfactoria. En INFOCOM, valoramos la opinión de nuestros clientes y queremos entender mejor lo sucedido para poder resolverlo y mejorar nuestros servicios.\n\nTe agradeceríamos si pudieras proporcionarnos más detalles sobre tu experiencia, ya sea sobre el producto o el servicio recibido. Tu opinión es muy importante para nosotros, y estamos comprometidos a solucionarlo.\n\nEsperamos tu respuesta y, nuevamente, te pedimos disculpas por los inconvenientes.\n\nAtentamente,\n\nEl equipo de INFOCOM Technology";
                Console.WriteLine("NEGATIVO");

            }
            var msg = MailHelper.CreateSingleEmail(from, to, subject, envio, envio);

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine($"Correo enviado con estado: {response.StatusCode}");
        }

        public async Task EnviarCorreoDeSuscripcionAsync(string email, string msj)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("no-reply@tu-dominio.com", "Infocom Technology");
            var to = new EmailAddress(email);
            string subject = "Confirmación de Suscripción";
            string envio = $"Hola,\nGracias por suscribirte a nuestro boletín. ¡Estás registrado con éxito!";

            if (!string.IsNullOrEmpty(msj))
            {
                envio = $"{msj}\n\nEstamos felices de tenerte con nosotros.";
            }

            var msg = MailHelper.CreateSingleEmail(from, to, subject, envio, envio);

            var response = await client.SendEmailAsync(msg);
            Log.Information($"Correo enviado con estado: {response.StatusCode}");
        }
    }
}