using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trabajo_Final.Data;
using Trabajo_Final.Models;
using Microsoft.EntityFrameworkCore;

namespace Trabajo_Final.Controllers
{
    [Route("[controller]")]
    public class HistorialComprasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HistorialComprasController> _logger;

        public HistorialComprasController(ApplicationDbContext context, ILogger<HistorialComprasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
      public async Task<IActionResult> Index(long? clienteId) // Mantener como long
        {
            if (clienteId == null)
            {
                await CrearDatosDePrueba();
                clienteId = 1; // Este ID debe corresponder al cliente creado
            }

            var compras = await _context.DataCompra
                .Where(c => c.ClienteId == clienteId) // Cambiar UsuarioId a ClienteId
                .Include(c => c.Detalles)
                .ThenInclude(d => d.Producto)
                .ToListAsync();

            return View(compras);
        }

        private async Task CrearDatosDePrueba()
        {
            if (!await _context.DataProducto.AnyAsync())
            {
                // Agregar productos de ejemplo
                var productos = new List<Producto>
                {
                    new Producto { Nombre = "Producto 1", Precio = 10.0M },
                    new Producto { Nombre = "Producto 2", Precio = 20.0M },
                    new Producto { Nombre = "Producto 3", Precio = 15.5M }
                };
                await _context.DataProducto.AddRangeAsync(productos);
                await _context.SaveChangesAsync();
            }

            if (!await _context.DataCliente.AnyAsync())
            {
                // Agregar cliente de ejemplo
                var cliente = new Cliente { Nombre = "Cliente Ejemplo" };
                await _context.DataCliente.AddAsync(cliente);
                await _context.SaveChangesAsync();

                // Crear una compra de prueba
                var compra = new Compra
                {
                    ClienteId = cliente.Id, // Asegúrate de usar ClienteId
                    FechaCompra = DateTime.UtcNow,
                    Detalles = new List<DetalleCompra>
                    {
                        new DetalleCompra {  Cantidad = 2, Precio = 10.0M },
                        new DetalleCompra {  Cantidad = 1, Precio = 20.0M }
                    }
                };
                await _context.DataCompra.AddAsync(compra);
                await _context.SaveChangesAsync();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
