using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trabajo_Final.Data;
using Trabajo_Final.Models;
using Microsoft.EntityFrameworkCore; 



namespace Trabajo_Final.Controllers

{
    [Route("[controller]")]
    public class HistorialComprasController : Controller
    {
    private readonly ApplicationDbContext _context; // Cambia esto si tu contexto tiene otro nombre
    private readonly ILogger<HistorialComprasController> _logger;

    public HistorialComprasController(ApplicationDbContext context, ILogger<HistorialComprasController> logger)
    {
        _context = context;
        _logger = logger;
    }

   [HttpGet]
    public async Task<IActionResult> Index(int usuarioId)
    {
            var compras = await _context.DataCompra
                .Where(c => c.UsuarioId == usuarioId)
                .Include(c => c.Detalles) // Incluye los detalles de cada compra
                .ThenInclude(d => d.Producto) // Incluye la información de producto en cada detalle
                .ToListAsync();

            return View(compras);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
}