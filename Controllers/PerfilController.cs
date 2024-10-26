using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Trabajo_Final.Controllers
{
    [Route("[controller]")]
public class PerfilController : Controller
{
    private readonly ILogger<PerfilController> _logger;

    public PerfilController(ILogger<PerfilController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
}