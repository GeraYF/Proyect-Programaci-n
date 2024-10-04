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
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ApplicationDbContext _context;

        public LoginController(ILogger<LoginController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ingresar(Cliente cliente)
        {
            var clientes = from o in _context.DataCliente select o;
            foreach (var c in clientes)
            {
                if (cliente.Email.Equals(c.Email))
                {
                    if (cliente.Contrasena.Equals(c.Contrasena))
                    {

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(nameof(Index));
        }

        [HttpPost]
        public IActionResult Registrar(Cliente cliente)
        {
            if (cliente.FechaNacimiento.Kind == DateTimeKind.Unspecified)
            {
                cliente.FechaNacimiento = DateTime.SpecifyKind(cliente.FechaNacimiento, DateTimeKind.Utc);
            }
            _context.Add(cliente);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}