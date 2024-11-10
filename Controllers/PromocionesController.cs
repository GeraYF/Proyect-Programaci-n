using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var categoria = _context.DataCategoria.Find(model.CategoriaId);
            model.FormPromociones.Categoria = categoria;
            if (model.FormPromociones.FechaInicio.Kind == DateTimeKind.Unspecified || model.FormPromociones.FechaFin.Kind == DateTimeKind.Unspecified)
            {
                model.FormPromociones.FechaInicio = DateTime.SpecifyKind(model.FormPromociones.FechaInicio, DateTimeKind.Utc);
                model.FormPromociones.FechaFin = DateTime.SpecifyKind(model.FormPromociones.FechaFin, DateTimeKind.Utc);
            }
            _context.Add(model.FormPromociones);
            var prod = _context.DataProducto
                            .Where(p => p.Categoria.Id == model.CategoriaId)
                            .ToList();
            foreach (var item in prod)
            {
                item.Descuento = true;
                item.PrecioAlternativo = (decimal)(item.Precio * ((model.FormPromociones.ValorDescuento / 100) + 1));
                _context.Update(item);

            }
            Console.WriteLine("APLICACIÓN TERMINADA");
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult IndexUpdate(long id)
        {
            var ListPromos = from o in _context.DataPromociones select o;
            var promocion = _context.DataPromociones.Include(p => p.Categoria).FirstOrDefault(p => p.Id == id);


            PromocionViewModel model = new PromocionViewModel
            {
                Promociones = ListPromos,
                Categorias = from o in _context.DataCategoria select o,
                FormPromociones = promocion,
                CategoriaId = promocion.Categoria.Id
            };
            ViewData["Action"] = "Update";
            return View("Index", model);
        }
        [HttpPost]
        public IActionResult Update(PromocionViewModel model)
        {
            var categoria = _context.DataCategoria.Find(model.CategoriaId);
            model.FormPromociones.Categoria = categoria;
            if (model.FormPromociones.FechaInicio.Kind == DateTimeKind.Unspecified || model.FormPromociones.FechaFin.Kind == DateTimeKind.Unspecified)
            {
                model.FormPromociones.FechaInicio = DateTime.SpecifyKind(model.FormPromociones.FechaInicio, DateTimeKind.Utc);
                model.FormPromociones.FechaFin = DateTime.SpecifyKind(model.FormPromociones.FechaFin, DateTimeKind.Utc);
            }
            if (!model.FormPromociones.Activo)
            {
                var prod = _context.DataProducto.Where(p => p.Categoria.Id == model.CategoriaId).ToList();
                foreach (var item in prod)
                {
                    item.Descuento = true;
                    item.PrecioAlternativo = (decimal)(item.Precio * ((model.FormPromociones.ValorDescuento / 100) + 1));
                    _context.Update(item);
                }
            }
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
                var prod = _context.DataProducto.Where(p => p.Categoria.Id == promocion.Categoria.Id).ToList();
                foreach (var item in prod)
                {
                    item.Descuento = false;
                    item.PrecioAlternativo = null;
                    _context.Update(item);
                }
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