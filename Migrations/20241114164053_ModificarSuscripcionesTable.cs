using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabajo_Final.Migrations
{
    /// <inheritdoc />
    public partial class ModificarSuscripcionesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agregar o modificar columnas aquí
            migrationBuilder.AddColumn<string>(
                name: "NewColumn",
                table: "t_suscripciones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar la columna si es necesario revertir la migración
            migrationBuilder.DropColumn(
                name: "NewColumn",
                table: "t_suscripciones");
        }
    }
}
