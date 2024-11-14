using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabajo_Final.Migrations
{
    /// <inheritdoc />
    public partial class SuscripcionId : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Crear la secuencia si no existe
        migrationBuilder.Sql(@"CREATE SEQUENCE IF NOT EXISTS t_suscripciones_id_seq 
                                START 1");

        // Alterar la columna "Id" para que use la secuencia como valor predeterminado
        migrationBuilder.AlterColumn<int>(
            name: "Id",
            table: "t_suscripciones",
            type: "int",
            nullable: false,
            defaultValueSql: "nextval('t_suscripciones_id_seq')", // Auto-generación del valor para "Id"
            oldClrType: typeof(int),
            oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Si deseas revertir este cambio, elimina la secuencia o restablece el comportamiento original
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "t_suscripciones",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // Eliminar la secuencia si se desea revertir todo
            migrationBuilder.Sql("DROP SEQUENCE IF EXISTS t_suscripciones_id_seq;");
        }
    }


}

