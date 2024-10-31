using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trabajo_Final.Data;
using Trabajo_Final.Models;

namespace Trabajo_Final.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ILogger<CarritoController> _logger;
        private readonly ApplicationDbContext _context;

        public CarritoController(ILogger<CarritoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var carritoItems = _context.DataCarrito.Where(c => c.UserName == User.Identity.Name).ToList();
            return View(carritoItems);
        }

        public IActionResult Agregar(long id)
        {
            var producto = _context.DataProducto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            var carrito = new Carrito
            {
                Producto = producto,
                Cantidad = 1,
                Precio = producto.Precio,
                UserName = User.Identity.Name // Assuming user is logged in
            };

            _context.DataCarrito.Add(carrito);
            _context.SaveChanges();

            return RedirectToAction("Index", "Catalogo");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}