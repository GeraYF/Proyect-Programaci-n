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
    public class AccionesAdminController : Controller
    {
        private readonly ILogger<AccionesAdminController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccionesAdminController(ILogger<AccionesAdminController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
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
                        Id = miUser.Id,
                        Name = miUser.UserName,
                        Email = miUser.Email,
                        PhoneNumber = miUser.PhoneNumber
                    });
                }
            }
            _logger.LogDebug("Administradores {administradores}", listaAdmin);
            return View(listaAdmin);
        }
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _logger.LogDebug("Usuario {Id} eliminado correctamente.", Id);
                Console.WriteLine("Admin Borrado");
                return RedirectToAction("Index");
            }
            return View("Error");
        }
        public async Task<IActionResult> IndexUsuario()
        {
            var users = _userManager.Users.ToList();
            var listUsuario = new List<UserViewModel>();
            foreach (var miUser in users)
            {
                var roles = await _userManager.GetRolesAsync(miUser);
                if (!roles.Contains("admin"))
                {
                    listUsuario.Add(new UserViewModel
                    {
                        Id = miUser.Id,
                        Name = miUser.UserName,
                        Email = miUser.Email,
                        EmailConfirmed = miUser.EmailConfirmed,
                        PhoneNumber = miUser.PhoneNumber
                    });
                }

            }
            _logger.LogDebug("Usuarios {Usuarios}", listUsuario);
            return View(listUsuario);
        }
        public async Task<IActionResult> Asignar(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var role = await _roleManager.FindByIdAsync("admin");
            var result = await _userManager.AddToRoleAsync(user, role.Name);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}