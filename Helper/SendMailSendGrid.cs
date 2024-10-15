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
        private readonly string? _apiKey = Environment.GetEnvironmentVariable("SEND_GRID_KEY");

        // Reemplaza con tu API Key

        public async Task EnviarCorreoAsync(string nombre, string emailTo, string subject, string body)
        {
            Console.WriteLine($"KEY:{_apiKey}");
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("infocomtechnologysoport@gmail.com", "Infocom Technology Soporte");
            var to = new EmailAddress(emailTo);
            string msj_envio = $"Hola{nombre}  \n!Gracias por contactarte con nosotros! Hemos recibido tu mensaje y queremos asegurarte que estamos revisando tu solicitud con atención.\nNuestro equipo está trabajando para brindarte una respuesta lo antes posible. Si tu consulta está relacionada con alguno de nuestros productos o servicios, puedes estar seguro de que te proporcionaremos toda la información que necesitas para tomar la mejor decisión.\nSi tienes más detalles que quieras compartir mientras tanto, no dudes en responder a este correo. Estamos aquí para ayudarte.\n¡Gracias por confiar en INFOCOM Technology!\nQue tengas un excelente día.";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, msj_envio, msj_envio);

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine($"Correo enviado con estado: {response.StatusCode}");
        }
    }
}