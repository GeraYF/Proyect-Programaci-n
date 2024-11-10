using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


using Trabajo_Final.Models;
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
    public DbSet<Trabajo_Final.Models.Promociones> DataPromociones { get; set; }
    public DbSet<Trabajo_Final.Models.DetalleCompra> DataDetalleCompra { get; set; }
    public DbSet<Trabajo_Final.Models.Comentario> DataComentario { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la relación entre Compra y DetalleCompra
        modelBuilder.Entity<DetalleCompra>()
            .HasOne(dc => dc.Compra)                // Relación entre DetalleCompra y Compra
            .WithMany(c => c.Detalles)             // Relación inversa entre Compra y DetalleCompra
            .HasForeignKey(dc => dc.CompraId)      // CompraId en DetalleCompra es la clave foránea
            .OnDelete(DeleteBehavior.Cascade);     // Comportamiento de eliminación (opcional)

        // Configuración de la relación entre DetalleCompra y Producto
        modelBuilder.Entity<DetalleCompra>()
            .HasOne(dc => dc.Producto)              // Relación entre DetalleCompra y Producto
            .WithMany()                            // Relación inversa no configurada explícitamente en Producto
            .HasForeignKey(dc => dc.ProductoId);   // ProductoId en DetalleCompra es la clave foránea


        modelBuilder.Entity<Compra>()
            .Property(c => c.FechaCompra)
            .HasConversion(
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc), // Convertir a UTC antes de guardar
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Leer como UTC
    }



}
