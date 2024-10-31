using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
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
                .ToList();

            ViewBag.Mensaje = mensaje; // Mostrar mensaje de éxito o error

            return View(carritoItems);
        }

        [HttpPost]
        public IActionResult EliminarProducto(int id)
        {
            // Encuentra el elemento del carrito por ID y verifica que pertenezca al usuario actual
            var carritoItem = _context.DataCarrito
                .FirstOrDefault(c => c.Id == id && c.UserName == User.Identity.Name);

            if (carritoItem != null)
            {
                _context.DataCarrito.Remove(carritoItem);
                _context.SaveChanges();

                return RedirectToAction("Index", new { mensaje = "Producto eliminado correctamente" });
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Agregar(long id)
        {
            var producto = _context.DataProducto.Find(id);
            if (producto != null)
            {
                var carritoItem = new Carrito
                {
                    UserName = User.Identity.Name,
                    Producto = producto,
                    Cantidad = 1,
                    Precio = producto.Precio
                };
                _context.DataCarrito.Add(carritoItem);
                _context.SaveChanges();
                return RedirectToAction("Index", new { mensaje = "Producto agregado correctamente" });
            }
            return NotFound();
        }
    }
}