using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Trabajo_Final.Models;

namespace Trabajo_Final.Controllers
{
    public class ApiTestController : Controller
    {
        private readonly ExternalApiService _externalApiService;

        public ApiTestController(ExternalApiService externalApiService)
        {
            _externalApiService = externalApiService;
        }

        // Acción para probar la obtención de productos
        public async Task<IActionResult> Productos()
        {
            var productos = await _externalApiService.GetProductsDataAsync();
            return View(productos);
        }

        // Acción para probar la obtención de clientes
        public async Task<IActionResult> Clientes()
        {
            var clientes = await _externalApiService.GetClientesDataAsync();
            return View(clientes);
        }
    }
}
