using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trabajo_Final.Data;
using Trabajo_Final.Models;
using Trabajo_Final.ViewModel;

namespace Trabajo_Final.Controllers
{
    public class PromocionesController : Controller
    {
        private readonly ILogger<PromocionesController> _logger;
        private readonly ApplicationDbContext _context;

        public PromocionesController(ILogger<PromocionesController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            PromocionViewModel model = new PromocionViewModel
            {
                Promociones = from o in _context.DataPromociones select o,
                Categorias = from o in _context.DataCategoria select o,
                FormPromociones = new Promociones()
            };
            ViewData["Action"] = "Create";
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(PromocionViewModel model)
        {
            Console.WriteLine("ENTRANDO A CREATE");
            var categoria = _context.DataCategoria.Find(model.CategoriaId);

            model.FormPromociones.Categoria = categoria;
            if (model.FormPromociones.FechaInicio.Kind == DateTimeKind.Unspecified || model.FormPromociones.FechaFin.Kind == DateTimeKind.Unspecified)
            {
                model.FormPromociones.FechaInicio = DateTime.SpecifyKind(model.FormPromociones.FechaInicio, DateTimeKind.Utc);
                model.FormPromociones.FechaFin = DateTime.SpecifyKind(model.FormPromociones.FechaFin, DateTimeKind.Utc);
            }
            _context.Add(model.FormPromociones);
            Console.WriteLine("GUARDADNDO OBJETO");
            Console.WriteLine("OBTENIENDO LISTA");
            var prod = _context.DataProducto
                            .Where(p => p.Categoria.Id == model.CategoriaId)
                            .ToList();
            foreach (var item in prod)
            {
                Console.WriteLine("ITERANDO");
                Console.WriteLine("HACIENDO VALIDACION");
                item.Precio = (decimal)(item.Precio - (item.Precio * (model.FormPromociones.ValorDescuento / 100)));
                _context.Update(item);

            }
            Console.WriteLine("APLICACIÓN TERMINADA");
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult IndexUpdate(long id)
        {
            var ListPromos = from o in _context.DataPromociones select o;
            var promo = _context.DataPromociones.Find(id);

            PromocionViewModel model = new PromocionViewModel
            {
                Promociones = ListPromos,
                Categorias = from o in _context.DataCategoria select o,
                FormPromociones = promo
            };
            ViewData["Action"] = "Update";
            return View("Index", model);
        }
        [HttpPost]
        public IActionResult Update(PromocionViewModel p)
        {
            if (p.FormPromociones.FechaInicio.Kind == DateTimeKind.Unspecified || p.FormPromociones.FechaFin.Kind == DateTimeKind.Unspecified)
            {
                p.FormPromociones.FechaInicio = DateTime.SpecifyKind(p.FormPromociones.FechaInicio, DateTimeKind.Utc);
                p.FormPromociones.FechaFin = DateTime.SpecifyKind(p.FormPromociones.FechaFin, DateTimeKind.Utc);
            }
            _context.Update(p.FormPromociones);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Delete(long id)
        {
            Console.WriteLine("ENTRABDO AL METODO DELETE");
            var promocion = _context.DataPromociones.Find(id);
            if (promocion == null)
            {
                return View("Error");
            }
            else
            {
                _context.Remove(promocion);
                _context.SaveChanges();
            }
            _logger.LogDebug($"Producto {id} eliminado");
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}