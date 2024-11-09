using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trabajo_Final.Models
{
    [Table("t_comentarios")]
    public class Comentario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Nombre { get; set; }
        public int? Calificacion { get; set; }
        public string? Cuerpo { get; set; }
        public Producto? Producto { get; set; }
    }
}