using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trabajo_Final.Models;

namespace Trabajo_Final.ViewModel
{
    public class CatalogoViewModel
    {
        public IEnumerable<Producto>? ListProducto { get; set; }
        public IEnumerable<Categoria>? ListCategoria { get; set; }
    }
}