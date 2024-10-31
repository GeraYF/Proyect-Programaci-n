using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Trabajo_Final.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Trabajo_Final.Models.Cliente> DataCliente { get; set; }
    public DbSet<Trabajo_Final.Models.Categoria> DataCategoria { get; set; }
    public DbSet<Trabajo_Final.Models.Contacto> DataContacto { get; set; }
    public DbSet<Trabajo_Final.Models.Producto> DataProducto { get; set; }
    public DbSet<Trabajo_Final.Models.Carrito> DataCarrito { get; set; }
    public DbSet<Trabajo_Final.Models.Compra> DataCompra { get; set; }
    public DbSet<Trabajo_Final.Models.DetalleCompra> DataDetalleCompra { get; set; }

    
}
