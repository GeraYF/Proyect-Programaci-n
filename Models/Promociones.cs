using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trabajo_Final.Models
{
    [Table("t_promociones")]
    public class Promociones
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Nombre { get; set; }
        public decimal? ValorDescuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Activo { get; set; } = true;
        public Categoria Categoria { get; set; }

    }
}