using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoEntityCore.Migrations
{
    public partial class RenombrarTablaYColumnaArticulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Articulos",
                table: "Articulos");

            migrationBuilder.RenameTable(
                name: "Articulos",
                newName: "tbl_Articulo");

            migrationBuilder.RenameColumn(
                name: "TituloArticulo",
                table: "tbl_Articulo",
                newName: "Articulo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_Articulo",
                table: "tbl_Articulo",
                column: "ArticuloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_Articulo",
                table: "tbl_Articulo");

            migrationBuilder.RenameTable(
                name: "tbl_Articulo",
                newName: "Articulos");

            migrationBuilder.RenameColumn(
                name: "Articulo",
                table: "Articulos",
                newName: "TituloArticulo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articulos",
                table: "Articulos",
                column: "ArticuloId");
        }
    }
}
