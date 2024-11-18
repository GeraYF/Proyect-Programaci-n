using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trabajo_Final.Data;
using Trabajo_Final.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        [Authorize]
        public async Task<IActionResult> Index(string categoria, string ordenar)
        {
            /*if (clienteId == null)
            {
                await CrearDatosDePrueba();
                var cliente = await _context.DataCliente.FirstOrDefaultAsync();
                /*if (cliente != null)
                {
                    clienteId = cliente.Id; // Obtener el ID del cliente creado
                }
            }*/
            string clienteId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var comprasQuery = _context.DataCompra
                .Where(c => c.ClienteId.Equals(clienteId))
                .Include(c => c.Detalles)
                .ThenInclude(d => d.Producto)
                .AsQueryable();

            if (long.TryParse(categoria, out long categoriaId))
            {
                comprasQuery = comprasQuery.Where(c => c.Detalles.Any(d => d.Producto.Categoria.Id == categoriaId));
            }

            if (ordenar == "asc")
            {
                comprasQuery = comprasQuery.OrderBy(c => c.FechaCompra);
            }
            else if (ordenar == "desc")
            {
                comprasQuery = comprasQuery.OrderByDescending(c => c.FechaCompra);
            }

            var compras = await comprasQuery.ToListAsync();

            ViewBag.Categorias = await _context.DataCategoria.ToListAsync();

            return View(compras);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EliminarTodo()
        {
            string clienteId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var compras = await _context.DataCompra
                .Where(c => c.ClienteId.Equals(clienteId))
                .Include(c => c.Detalles)
                .ToListAsync();

            foreach (var compra in compras)
            {
                _context.DataDetalleCompra.RemoveRange(compra.Detalles);
                _context.DataCompra.Remove(compra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task CrearDatosDePrueba()
        {
            List<Producto> productos = await _context.DataProducto.ToListAsync();

            if (!productos.Any())
            {
                // Agregar productos de ejemplo
                productos = new List<Producto>
                {
                    new Producto { Nombre = "Producto 1", Precio = 10.0M, Categoria = new Categoria { Nombre = "Categoria 1" } },
                    new Producto { Nombre = "Producto 2", Precio = 20.0M, Categoria = new Categoria { Nombre = "Categoria 2" } },
                    new Producto { Nombre = "Producto 3", Precio = 15.5M, Categoria = new Categoria { Nombre = "Categoria 3" } }
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
                /* var compra = new Compra
                 {
                     ClienteId = //cliente.Id,
                     FechaCompra = DateTime.UtcNow,
                     Estado = "Entregado", // Set the order status
                     Detalles = new List<DetalleCompra>
                     {
                         new DetalleCompra { Producto = productos[0], Cantidad = 2, PrecioUnitario = 10.0M },
                         new DetalleCompra { Producto = productos[1], Cantidad = 1, PrecioUnitario = 20.0M }
                     }
                 };*/
                // await _context.DataCompra.AddAsync(compra);
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
