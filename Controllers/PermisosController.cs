using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Trabajo_Final.ViewModel;


namespace Trabajo_Final.Controllers
{
    public class PermisosController : Controller
    {
        private readonly ILogger<PermisosController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public PermisosController(ILogger<PermisosController> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var listaAdmin = new List<AdminViewModel>();
            foreach (var miUser in users)
            {
                var roles = await _userManager.GetRolesAsync(miUser);
                if (roles.Contains("admin"))
                {
                    listaAdmin.Add(new AdminViewModel
                    {
                        Name = miUser.UserName,
                        Email = miUser.Email,
                        PhoneNumber = miUser.PhoneNumber,
                    });
                }
            }
            _logger.LogDebug("Administradores {administradores}", listaAdmin);
            return View(listaAdmin);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}