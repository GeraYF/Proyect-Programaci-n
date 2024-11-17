// using Microsoft.EntityFrameworkCore.Migrations;
// using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

// #nullable disable

// namespace Trabajo_Final.Data.Migrations
// {
//     /// <inheritdoc />
//     public partial class MigracionPerfil : Migration
//     {
//         /// <inheritdoc />
//         protected override void Up(MigrationBuilder migrationBuilder)
//         {
//             migrationBuilder.CreateTable(
//                 name: "t_perfil",
//                 columns: table => new
//                 {
//                     Id = table.Column<long>(type: "bigint", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Nombre = table.Column<string>(type: "text", nullable: true),
//                     Apellido = table.Column<string>(type: "text", nullable: true),
//                     Direccion = table.Column<string>(type: "text", nullable: true),
//                     Ciudad = table.Column<string>(type: "text", nullable: true),
//                     Pais = table.Column<string>(type: "text", nullable: true),
//                     CodigoPostal = table.Column<string>(type: "text", nullable: true),
//                     UserName = table.Column<string>(type: "text", nullable: true),
//                     Email = table.Column<string>(type: "text", nullable: true),
//                     PhoneNumber = table.Column<string>(type: "text", nullable: true)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_t_perfil", x => x.Id);
//                 });
//         }

//         /// <inheritdoc />
//         protected override void Down(MigrationBuilder migrationBuilder)
//         {
//             migrationBuilder.DropTable(
//                 name: "t_perfil");
//         }
//     }
// }
