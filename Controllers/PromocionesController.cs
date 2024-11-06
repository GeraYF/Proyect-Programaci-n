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
                FormPromociones = new Promociones()
            };
            ViewData["Action"] = "Create";
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(PromocionViewModel p)
        {
            if (p.FormPromociones.FechaInicio.Kind == DateTimeKind.Unspecified || p.FormPromociones.FechaFin.Kind == DateTimeKind.Unspecified)
            {
                p.FormPromociones.FechaInicio = DateTime.SpecifyKind(p.FormPromociones.FechaInicio, DateTimeKind.Utc);
                p.FormPromociones.FechaFin = DateTime.SpecifyKind(p.FormPromociones.FechaFin, DateTimeKind.Utc);
            }
            _context.Add(p.FormPromociones);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}