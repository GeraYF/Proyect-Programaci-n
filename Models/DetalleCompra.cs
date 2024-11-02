using System.ComponentModel.DataAnnotations.Schema;

namespace Trabajo_Final.Models
{
    [Table("t_detalle_compra")]
    public class DetalleCompra
    {
        public int Id { get; set; }
        public int CompraId { get; set; }
        [ForeignKey("CompraId")]
        public Compra Compra { get; set; }
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Precio { get; set; }
    }
}

