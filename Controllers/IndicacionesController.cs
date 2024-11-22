using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trabajo_Final.Models;

namespace Trabajo_Final.Controllers
{
    public class IndicacionesController : Controller
    {
        // Acción para manejar la solicitud GET a /Indicaciones
        public IActionResult Index()
        {
            return View();
        }
    }
}