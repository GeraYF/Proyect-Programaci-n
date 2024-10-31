using Microsoft.AspNetCore.Identity;

namespace Trabajo_Final.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Agrega propiedades adicionales si es necesario
        public string? NombreCompleto { get; set; } // Ejemplo de propiedad adicional
        // Puedes agregar más propiedades personalizadas aquí
    }
}
