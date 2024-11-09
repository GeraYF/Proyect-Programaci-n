using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Trabajo_Final.Migrations
{
    /// <inheritdoc />
    public partial class AgregarProductoIdADetalleCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "t_carrito");

            migrationBuilder.DropTable(
                name: "t_cliente");

            migrationBuilder.DropTable(
                name: "t_comentarios");

            migrationBuilder.DropTable(
                name: "t_contacto");

            migrationBuilder.DropTable(
                name: "t_detalle_compra");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "t_compra");

            migrationBuilder.DropTable(
                name: "t_producto");

            migrationBuilder.DropTable(
                name: "t_categoria");
        }
    }
}
