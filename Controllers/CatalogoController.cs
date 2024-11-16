using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Trabajo_Final.Data;
using Trabajo_Final.ViewModel;

namespace Trabajo_Final.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;
        private readonly ApplicationDbContext _context;

        public CatalogoController(ILogger<CatalogoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            var productos = from o in _context.DataProducto select o;
            var categorias = from o in _context.DataCategoria select o;

            if (!String.IsNullOrEmpty(searchString))
            {
                productos = productos.Where(s => s.Nombre.Contains(searchString));
            }

            var viewModel = new CatalogoViewModel
            {
                ListCategoria = categorias,
                ListProducto = productos
            };
            _logger.LogDebug("Producto {productos}", productos);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Detalles(int id)
        {
            var producto = _context.DataProducto
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.Id == id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            var viewModel = new ProductoViewModel
            {
                FormProducto = producto
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}