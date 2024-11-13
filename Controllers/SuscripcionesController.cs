using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trabajo_Final.Data;
using Trabajo_Final.ViewModel;
using SendGrid;
using SendGrid.Helpers.Mail;
using Trabajo_Final.Models; 
using System.ComponentModel.DataAnnotations; 

namespace Trabajo_Final.Controllers
{
    [Route("[controller]")]
    public class SuscripcionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public SuscripcionController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Suscribir(string email)
        {
            if (string.IsNullOrEmpty(email) || !new EmailAddressAttribute().IsValid(email))
            {
                return BadRequest("Correo electrónico no válido.");
            }

            // Guardar en la base de datos
            var suscripcion = new Suscripciones { Email = email };
            _context.DataSuscripciones.Add(suscripcion);
            await _context.SaveChangesAsync();

            // Enviar confirmación con SendGrid
            var sendGridKey = _configuration["SendGrid:ApiKey"];
            if (!string.IsNullOrEmpty(sendGridKey))
            {
                var client = new SendGridClient(sendGridKey);
                var from = new EmailAddress("no-reply@tu-dominio.com", "Tienda Ejemplo");
                var subject = "Gracias por suscribirse";
                var to = new EmailAddress(email);
                var plainTextContent = "Gracias por suscribirte a nuestra tienda.";
                var htmlContent = "<strong>Gracias por suscribirte a nuestra tienda.</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                await client.SendEmailAsync(msg);
            }

            return Ok("Suscripción exitosa.");
        }
    }
}
