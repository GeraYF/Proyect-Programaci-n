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
        public int UsuarioId { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Total { get; set; }
        public List<DetalleCompra>? Detalles { get; set; } // Relación con los detalles
    }

}