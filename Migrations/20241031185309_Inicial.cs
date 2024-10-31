using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Trabajo_Final.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<long>(
                name: "ProductoId1",
                table: "t_detalleCompra",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_detalleCompra_CompraId",
                table: "t_detalleCompra",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_t_detalleCompra_ProductoId1",
                table: "t_detalleCompra",
                column: "ProductoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_t_detalleCompra_t_compra_CompraId",
                table: "t_detalleCompra",
                column: "CompraId",
                principalTable: "t_compra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_detalleCompra_t_producto_ProductoId1",
                table: "t_detalleCompra",
                column: "ProductoId1",
                principalTable: "t_producto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_detalleCompra_t_compra_CompraId",
                table: "t_detalleCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_t_detalleCompra_t_producto_ProductoId1",
                table: "t_detalleCompra");

            migrationBuilder.DropIndex(
                name: "IX_t_detalleCompra_CompraId",
                table: "t_detalleCompra");

            migrationBuilder.DropIndex(
                name: "IX_t_detalleCompra_ProductoId1",
                table: "t_detalleCompra");

            migrationBuilder.DropColumn(
                name: "ProductoId1",
                table: "t_detalleCompra");

            migrationBuilder.DropColumn(
                name: "NombreCompleto",
                table: "AspNetUsers");

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
    }
}
