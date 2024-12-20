using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trabajo_Final.Data;
using Trabajo_Final.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims; // Add this using directive

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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var carritoItems = await _context.DataCarrito
                .Where(c => c.UserName == UserName).Include(c => c.Producto).ToListAsync();

            Compra compra = new Compra()
            {
                ClienteId = userId,
                Total = carritoItems.Sum(c => c.Precio)
            };
            List<DetalleCompra> listDet = new List<DetalleCompra>();
            foreach (var c in carritoItems)
            {
                DetalleCompra det = new DetalleCompra()
                {
                    CompraId = compra.Id,
                    Compra = compra,
                    ProductoId = c.Producto.Id,
                    Producto = c.Producto,
                    Cantidad = c.Cantidad,

                    PrecioUnitario = c.Precio
                };
                listDet.Add(det);
            }
            compra.Detalles = listDet;
            _context.Add(compra);
            _context.SaveChanges();

            return View("Confirmacion");
        }

        // GET: /Pago/Confirmacion
        public IActionResult Confirmacion()
        {
            return View(); // Devuelve una vista de confirmación
        }
    }
}
