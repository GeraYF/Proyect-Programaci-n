using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Trabajo_Final.Data;
using Trabajo_Final.Models;
using Trabajo_Final.ViewModel;

namespace Trabajo_Final.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ILogger<ProductosController> _logger;
        private readonly ApplicationDbContext _context;

        public ProductosController(ILogger<ProductosController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var categorias = from o in _context.DataCategoria select o;
            var productos = from o in _context.DataProducto select o;
            var viewModel = new ProductoViewModel
            {
                ListCategoria = categorias,
                ListProducto = productos,
                FormProducto = new Producto()
            };
            ViewData["Action"] = "Registrar";
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Registrar(ProductoViewModel p)
        {
            var categoria = _context.DataCategoria.Find(p.CategoriaId);
            if (p.FormProducto.FechaLanzamiento.Kind == DateTimeKind.Unspecified)
            {
                p.FormProducto.FechaLanzamiento = DateTime.SpecifyKind(p.FormProducto.FechaLanzamiento, DateTimeKind.Utc);
            }
            p.FormProducto.Categoria = categoria;
            _context.Add(p.FormProducto);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(long Id)
        {
            var producto = _context.DataProducto.Find(Id);
            if (producto != null)
            {
                _context.DataProducto.Remove(producto);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Editar(long Id)
        {
            var categorias = from o in _context.DataCategoria select o;
            var producto = _context.DataProducto.Find(Id);
            var productos = from o in _context.DataProducto select o;
            var viewModel = new ProductoViewModel
            {
                ListProducto = productos,
                ListCategoria = categorias,
                FormProducto = producto
            };
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["Action"] = "Actualizar";
            return View("~/Views/Productos/Index.cshtml", viewModel);
        }

        [HttpPost]
        public IActionResult Actualizar(ProductoViewModel p)
        {
            var categoria = _context.DataCategoria.Find(p.CategoriaId);
            if (p.FormProducto.FechaLanzamiento.Kind == DateTimeKind.Unspecified)
            {
                p.FormProducto.FechaLanzamiento = DateTime.SpecifyKind(p.FormProducto.FechaLanzamiento, DateTimeKind.Utc);
            }
            p.FormProducto.Categoria = categoria;
            _context.DataProducto.Update(p.FormProducto);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
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

        [HttpPost]
        public IActionResult AgregarAlCarrito(long productoId)
        {
            if (productoId <= 0)
            {
                return BadRequest("ID de producto no válido.");
            }

            var producto = _context.DataProducto.Find(productoId);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            var carritoItemExistente = _context.DataCarrito
                .FirstOrDefault(c => c.Producto.Id == producto.Id && c.UserName == User.Identity.Name);

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
                _context.DataCarrito.Add(carritoItem);
            }

            _context.SaveChanges();
            return RedirectToAction("Detalles", new { id = productoId, mensaje = "Producto agregado al carrito." });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}