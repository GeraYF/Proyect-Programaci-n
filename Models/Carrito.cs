using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trabajo_Final.Models
{
    [Table("t_carrito")]
    public class Carrito
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public Producto? Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Status { get; set; } = "PENDIENTE";

    }
}