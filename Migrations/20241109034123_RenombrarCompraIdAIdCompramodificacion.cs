using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trabajo_Final.Migrations
{
    /// <inheritdoc />
    public partial class RenombrarCompraIdAIdCompramodificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_detalle_compra_t_compra_IdCompra",
                table: "t_detalle_compra");

            migrationBuilder.RenameColumn(
                name: "IdCompra",
                table: "t_detalle_compra",
                newName: "CompraId");

            migrationBuilder.RenameIndex(
                name: "IX_t_detalle_compra_IdCompra",
                table: "t_detalle_compra",
                newName: "IX_t_detalle_compra_CompraId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_detalle_compra_t_compra_CompraId",
                table: "t_detalle_compra",
                column: "CompraId",
                principalTable: "t_compra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_detalle_compra_t_compra_CompraId",
                table: "t_detalle_compra");

            migrationBuilder.RenameColumn(
                name: "CompraId",
                table: "t_detalle_compra",
                newName: "IdCompra");

            migrationBuilder.RenameIndex(
                name: "IX_t_detalle_compra_CompraId",
                table: "t_detalle_compra",
                newName: "IX_t_detalle_compra_IdCompra");

            migrationBuilder.AddForeignKey(
                name: "FK_t_detalle_compra_t_compra_IdCompra",
                table: "t_detalle_compra",
                column: "IdCompra",
                principalTable: "t_compra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
