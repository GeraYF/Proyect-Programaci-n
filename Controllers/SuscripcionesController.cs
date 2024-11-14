using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Trabajo_Final.Data;
using Trabajo_Final.Helper;
using Trabajo_Final.Models;

namespace Trabajo_Final.Controllers
{
    [Route("[controller]")]
    public class SuscripcionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SuscripcionEmailService _emailService;

        public SuscripcionController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;

            // Obtén la clave API de SendGrid de la variable de entorno
            var apiKey = Environment.GetEnvironmentVariable("KEYSEND");
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey), "La clave API de SendGrid no puede estar vacía.");

            // Inicializa el servicio de envío de correo con la clave API
            _emailService = new SuscripcionEmailService(apiKey);
        }

        [HttpPost]
        public async Task<IActionResult> Suscribir(string email)
        {
            if (string.IsNullOrEmpty(email) || !new EmailAddressAttribute().IsValid(email))
            {
                return BadRequest("Correo electrónico no válido.");
            }

            // Crear la nueva suscripción y solo asignar el email
            var suscripcion = new Suscripciones { Email = email };

            // El campo Id se generará automáticamente
            _context.DataSuscripciones.Add(suscripcion);
            await _context.SaveChangesAsync();

            // Enviar el correo de confirmación con SendGrid
            string msj = "Gracias por suscribirte"; // Mensaje de confirmación
            await _emailService.EnviarCorreoDeSuscripcionAsync(email, msj);

            return Ok("Suscripción exitosa.");
        }
    }
}
