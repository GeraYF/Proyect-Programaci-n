using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trabajo_Final.Models
{
    [Table("t_detalleCompra")]
    public class DetalleCompra
    {
        public long Id { get; set; }
        public long CompraId { get; set; }
        public long ProductoId { get; set; }
        public Producto? Producto { get; set; } 

        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

        
    }

}