using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trabajo_Final.Data;
using Trabajo_Final.Models;
using Microsoft.EntityFrameworkCore; // Add this using directive

namespace Trabajo_Final.Controllers
{
    public class PagoController : Controller
    {
        private readonly ApplicationDbContext _context; // Add this field

        public PagoController(ApplicationDbContext context) // Modify constructor
        {
            _context = context;
        }

        // GET: /Pago
        public IActionResult Index()
        {
            return View(); // Devuelve la vista de pago
        }

        // POST: /Pago/ProcesarPago
        [HttpPost]
        public async Task<IActionResult> ProcesarPago(string NombreCompleto, string CorreoElectronico, string Direccion, string Ciudad, string CodigoPostal, string NumeroTarjeta, string FechaExpiracion, string CVV, string UserName)
        {
            // Aquí manejarías la lógica para procesar el pago
            // Por ahora, solo redirigimos a una vista de confirmación o a una página de éxito

            // Clear the cart after payment is processed
            var carritoItems = await _context.DataCarrito
                .Where(c => c.UserName == UserName)
                .ToListAsync();
            _context.DataCarrito.RemoveRange(carritoItems);
            await _context.SaveChangesAsync();

            return RedirectToAction("Confirmacion");
        }

        // GET: /Pago/Confirmacion
        public IActionResult Confirmacion()
        {
            return View(); // Devuelve una vista de confirmación
        }
    }
}
