using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Trabajo_Final.Data;
using Trabajo_Final.Models;

namespace Trabajo_Final.Services
{
    public class ProductoService
    {
        private readonly ILogger<ProductoService> _logger;
        private readonly ApplicationDbContext _context;

        public ProductoService(ILogger<ProductoService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<Producto>?> GetAll()
        {
            if (_context.DataProducto == null)
                return null;
            var productos = await _context.DataProducto.ToListAsync();
            _logger.LogInformation("Productos: {0}", productos);
            return productos;
        }

        public async Task<Producto?> Get(long? id)
        {
            if (id == null || _context.DataProducto == null)
            {
                return null;
            }

            var producto = await _context.DataProducto.FindAsync(id);
            _logger.LogInformation("Producto: {0}", producto);
            if (producto == null)
            {
                return null;
            }
            return producto;
        }
    }
}