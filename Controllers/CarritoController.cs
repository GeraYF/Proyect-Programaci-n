using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabajo_Final.Data;
using Trabajo_Final.Models;
using Microsoft.AspNetCore.Authorization;

namespace Trabajo_Final.Controllers
{
    [Authorize]
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarritoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string mensaje)
        {
            var carritoItems = _context.DataCarrito
                .Where(c => c.UserName == User.Identity.Name)
                .Include(c => c.Producto)
                .ToList();

            ViewBag.Mensaje = mensaje;
            return View(carritoItems);
        }

        [HttpPost]
        public IActionResult EliminarProducto(int id)
        {
            var carritoItem = _context.DataCarrito
                .FirstOrDefault(c => c.Id == id && c.UserName == User.Identity.Name);

            if (carritoItem != null)
            {
                _context.DataCarrito.Remove(carritoItem);
                _context.SaveChanges();

                return RedirectToAction("Index", new { mensaje = "Producto eliminado correctamente." });
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarAlCarrito(long productoId)
        {
            if (productoId <= 0)
            {
                return BadRequest("ID de producto no válido.");
            }

            var producto = _context.DataProducto.Find(productoId);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            var carritoItemExistente = _context.DataCarrito
                .FirstOrDefault(c => c.Producto.Id == producto.Id && c.UserName == User.Identity.Name);

            if (carritoItemExistente != null)
            {
                carritoItemExistente.Cantidad += 1;
                _context.DataCarrito.Update(carritoItemExistente);
            }
            else
            {
                var carritoItem = new Carrito
                {
                    UserName = User.Identity.Name,
                    Producto = producto,
                    Cantidad = 1,
                    Precio = producto.Precio
                };
                _context.DataCarrito.Add(carritoItem);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", new { mensaje = "Producto agregado correctamente." });
        }

        public IActionResult ProcederAlPago()
        {
            var carritoItems = _context.DataCarrito
                .Where(c => c.UserName == User.Identity.Name)
                .ToList();

            if (!carritoItems.Any())
            {
                return RedirectToAction("Index", new { mensaje = "No hay productos en el carrito para proceder al pago." });
            }

            return RedirectToAction("Index", "Pago");
        }

        public IActionResult DescargarFactura()
        {
            var carritoItems = _context.DataCarrito
                .Where(c => c.UserName == User.Identity.Name)
                .Include(c => c.Producto)
                .ToList();

            using (var memoryStream = new MemoryStream())
            {
                // Configuración de documento y escritor de PDF
                var document = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Fuentes personalizadas
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
                var bodyFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.DARK_GRAY);
                var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);

                // Título de la factura
                var title = new Paragraph("Factura de Compra", titleFont) { Alignment = Element.ALIGN_CENTER };
                document.Add(title);
                document.Add(new Paragraph($"Fecha: {System.DateTime.Now:dd/MM/yyyy}", bodyFont));
                document.Add(new Paragraph("Cliente: " + User.Identity.Name, bodyFont));
                document.Add(Chunk.NEWLINE);

                // Tabla de productos
                var table = new PdfPTable(4) { WidthPercentage = 100 };
                table.SetWidths(new float[] { 15f, 45f, 20f, 20f });

                // Encabezados de tabla con fondo de color
                var headers = new string[] { "Cantidad", "Descripción", "Precio Unitario", "Total" };
                foreach (var header in headers)
                {
                    var cell = new PdfPCell(new Phrase(header, headerFont))
                    {
                        BackgroundColor = new BaseColor(63, 81, 181), // Fondo azul oscuro
                        Padding = 5,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    table.AddCell(cell);
                }

                // Filas de productos
                decimal totalAmount = 0;
                foreach (var item in carritoItems)
                {
                    table.AddCell(new PdfPCell(new Phrase(item.Cantidad.ToString(), bodyFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase(item.Producto.Nombre, bodyFont)));
                    table.AddCell(new PdfPCell(new Phrase($"${item.Precio:F2}", bodyFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    table.AddCell(new PdfPCell(new Phrase($"${item.Cantidad * item.Precio:F2}", bodyFont)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    totalAmount += item.Cantidad * item.Precio;
                }

                document.Add(table);
                document.Add(Chunk.NEWLINE);

                // Total a pagar, alineado a la derecha
                var totalParagraph = new Paragraph($"Total: ${totalAmount:F2}", titleFont) { Alignment = Element.ALIGN_RIGHT };
                document.Add(totalParagraph);
                document.Add(Chunk.NEWLINE);

                // Mensaje de agradecimiento
                var thankYou = new Paragraph("Gracias por su compra. ¡Esperamos verle pronto!", bodyFont) { Alignment = Element.ALIGN_CENTER };
                document.Add(thankYou);

                document.Close();
                _context.DataCarrito.RemoveRange(carritoItems);
                _context.SaveChanges();

                // Descargar el archivo PDF
                return File(memoryStream.ToArray(), "application/pdf", "Factura.pdf");
            }
        }
    }
}