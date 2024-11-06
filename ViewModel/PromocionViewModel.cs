using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trabajo_Final.Models;

namespace Trabajo_Final.ViewModel
{
    public class PromocionViewModel
    {

        public IEnumerable<Promociones> Promociones { get; set; }
        public IEnumerable<Categoria> Categorias { get; set; }
        public Promociones? FormPromociones { get; set; }
        public long? CategoriaId { get; set; }
    }
}