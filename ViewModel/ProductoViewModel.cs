using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trabajo_Final.Models;

namespace Trabajo_Final.ViewModel
{
    public class ProductoViewModel
    {
        public IEnumerable<Categoria>? ListCategoria { get; set; }
        public IEnumerable<Producto>? ListProducto { get; set; }
        public Producto? FormProducto { get; set; }
        public long? CategoriaId { get; set; }
    }
}