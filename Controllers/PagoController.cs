using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trabajo_Final.Data;
using Trabajo_Final.Models;

namespace Trabajo_Final.Controllers
{
    public class PagoController : Controller
    {
        // GET: /Pago
        public IActionResult Index()
        {
            return View(); // Devuelve la vista de pago
        }

        // POST: /Pago/ProcesarPago
        [HttpPost]
        public IActionResult ProcesarPago(string NombreCompleto, string CorreoElectronico, string Direccion, string Ciudad, string CodigoPostal, string NumeroTarjeta, string FechaExpiracion, string CVV)
        {
            // Aquí manejarías la lógica para procesar el pago
            // Por ahora, solo redirigimos a una vista de confirmación o a una página de éxito
            return RedirectToAction("Confirmacion");
        }

        // GET: /Pago/Confirmacion
        public IActionResult Confirmacion()
        {
            return View(); // Devuelve una vista de confirmación
        }
    }
}
