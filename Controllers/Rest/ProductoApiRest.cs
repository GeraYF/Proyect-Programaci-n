using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trabajo_Final.Models;
using Trabajo_Final.Services;

namespace Trabajo_Final.Controllers.Rest
{
    [ApiController]
    [Route("api/producto")]
    public class ProductoApiRest : ControllerBase
    {
        private readonly ILogger<ProductoApiRest> _logger;
        private readonly ProductoService _productoService;

        public ProductoApiRest(ILogger<ProductoApiRest> logger, ProductoService productoService)
        {
            _logger = logger;
            _productoService = productoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Producto>>> GetProductos()
        {
            var productos = await _productoService.GetAll();
            _logger.LogInformation("GetProductos{0}", productos);
            if (productos == null)
                return NotFound();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Producto>> GetProducto(long? id)
        {
            var producto = await _productoService.Get(id);
            if (producto == null)
                return NotFound();
            return Ok(producto);
        }

    }
}