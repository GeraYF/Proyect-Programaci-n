using System.ComponentModel.DataAnnotations.Schema;

namespace Trabajo_Final.Models
{
        [Table("t_detalle_compra")]
        public class DetalleCompra
        {
                [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
                public long Id { get; set; }
                public Compra Compra { get; set; }
                public Producto Producto { get; set; }
                public int? Cantidad { get; set; }
                public decimal? PrecioUnitario { get; set; }
                public decimal? Precio { get; set; }
        }
}

