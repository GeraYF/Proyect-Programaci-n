using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trabajo_Final.Models
{
    [Table("t_producto")]
    public class Producto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Nombre { get; set; }
        public string? Description { get; set; }
        public Decimal Precio { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public int Calificacion { get; set; }
        public string? Status { get; set; }
        public string? ImageURL { get; set; }
        public Categoria? Categoria { get; set; }
        public Decimal? PrecioAlternativo { get; set; }
        public bool Descuento { get; set; } = false;
    }
}