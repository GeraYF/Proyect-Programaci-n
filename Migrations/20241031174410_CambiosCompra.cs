using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Trabajo_Final.Migrations
{
    /// <inheritdoc />
    public partial class CambiosCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_detalleCompra_t_compra_CompraId",
                table: "t_detalleCompra");

            migrationBuilder.DropIndex(
                name: "IX_t_detalleCompra_CompraId",
                table: "t_detalleCompra");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "t_compra");

            migrationBuilder.AlterColumn<long>(
                name: "ProductoId",
                table: "t_detalleCompra",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "CompraId",
                table: "t_detalleCompra",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "t_detalleCompra",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "CompraId1",
                table: "t_detalleCompra",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ClienteId",
                table: "t_compra",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_t_detalleCompra_CompraId1",
                table: "t_detalleCompra",
                column: "CompraId1");

            migrationBuilder.CreateIndex(
                name: "IX_t_detalleCompra_ProductoId",
                table: "t_detalleCompra",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_detalleCompra_t_compra_CompraId1",
                table: "t_detalleCompra",
                column: "CompraId1",
                principalTable: "t_compra",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_detalleCompra_t_producto_ProductoId",
                table: "t_detalleCompra",
                column: "ProductoId",
                principalTable: "t_producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_detalleCompra_t_compra_CompraId1",
                table: "t_detalleCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_t_detalleCompra_t_producto_ProductoId",
                table: "t_detalleCompra");

            migrationBuilder.DropIndex(
                name: "IX_t_detalleCompra_CompraId1",
                table: "t_detalleCompra");

            migrationBuilder.DropIndex(
                name: "IX_t_detalleCompra_ProductoId",
                table: "t_detalleCompra");

            migrationBuilder.DropColumn(
                name: "CompraId1",
                table: "t_detalleCompra");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "t_compra");

            migrationBuilder.AlterColumn<int>(
                name: "ProductoId",
                table: "t_detalleCompra",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CompraId",
                table: "t_detalleCompra",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "t_detalleCompra",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "t_compra",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_t_detalleCompra_CompraId",
                table: "t_detalleCompra",
                column: "CompraId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_detalleCompra_t_compra_CompraId",
                table: "t_detalleCompra",
                column: "CompraId",
                principalTable: "t_compra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
