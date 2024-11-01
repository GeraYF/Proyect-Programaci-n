using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabajo_Final.Data;
using Trabajo_Final.Models;

namespace Trabajo_Final.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarritoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string mensaje)
        {
            var carritoItems = _context.DataCarrito
                .Where(c => c.UserName == User.Identity.Name)
                .Include(c => c.Producto)  // Asegura que se cargue el producto asociado
                .ToList();

            ViewBag.Mensaje = mensaje;

            return View(carritoItems);
        }

        [HttpPost]
        public IActionResult EliminarProducto(int id)
        {
            var carritoItem = _context.DataCarrito
                .FirstOrDefault(c => c.Id == id && c.UserName == User.Identity.Name);

            if (carritoItem != null)
            {
                _context.DataCarrito.Remove(carritoItem);
                _context.SaveChanges();

                return RedirectToAction("Index", new { mensaje = "Producto eliminado correctamente." });
            }

            return NotFound();
        }

        public async Task<IActionResult> Agregar(long id)
        {
            if (id <= 0)
            {
                return BadRequest("ID de producto no válido.");
            }

            var producto = await _context.DataProducto.FindAsync(id);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            var carritoItemExistente = await _context.DataCarrito
                .FirstOrDefaultAsync(c => c.Producto.Id == producto.Id && c.UserName == User.Identity.Name);

            if (carritoItemExistente != null)
            {
                carritoItemExistente.Cantidad += 1;
                _context.DataCarrito.Update(carritoItemExistente);
            }
            else
            {
                var carritoItem = new Carrito
                {
                    UserName = User.Identity.Name,
                    Producto = producto,
                    Cantidad = 1,
                    Precio = producto.Precio
                };
                await _context.DataCarrito.AddAsync(carritoItem);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { mensaje = "Producto agregado correctamente." });
        }

        public IActionResult ProcederAlPago()
        {
            // Puedes agregar lógica aquí para validar que hay productos en el carrito
            var carritoItems = _context.DataCarrito
                .Where(c => c.UserName == User.Identity.Name)
                .ToList();

            if (!carritoItems.Any())
            {
                return RedirectToAction("Index", new { mensaje = "No hay productos en el carrito para proceder al pago." });
            }

            return RedirectToAction("Index", "Pago"); // Asegúrate de que "Pago" es el nombre correcto de tu controlador de pago
        }
    }
}