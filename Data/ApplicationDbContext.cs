using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


using Trabajo_Final.Models;
namespace Trabajo_Final.Data;

 public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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
    public DbSet<Trabajo_Final.Models.DetalleCompra> DataDetalleCompra { get; set; }


   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Compra>()
            .Property(c => c.FechaCompra)
            .HasConversion(
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc), // Convertir a UTC antes de guardar
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Leer como UTC
    }



}
