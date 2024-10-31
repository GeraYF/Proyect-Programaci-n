using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trabajo_Final.Models
{
    [Table("t_compra")]
    public class Compra
    {
        public int Id { get; set; }
        public long ClienteId { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Total { get; set; }
       public IEnumerable<DetalleCompra> Detalles { get; set; } = new List<DetalleCompra>();

    }

}