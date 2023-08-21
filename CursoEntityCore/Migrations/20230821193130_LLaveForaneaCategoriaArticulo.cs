using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoEntityCore.Migrations
{
    public partial class LLaveForaneaCategoriaArticulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categorias",
                newName: "Categoria_Id");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "tbl_Articulo",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Categoria_Id",
                table: "tbl_Articulo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Articulo_Categoria_Id",
                table: "tbl_Articulo",
                column: "Categoria_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Articulo_Categorias_Categoria_Id",
                table: "tbl_Articulo",
                column: "Categoria_Id",
                principalTable: "Categorias",
                principalColumn: "Categoria_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Articulo_Categorias_Categoria_Id",
                table: "tbl_Articulo");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Articulo_Categoria_Id",
                table: "tbl_Articulo");

            migrationBuilder.DropColumn(
                name: "Categoria_Id",
                table: "tbl_Articulo");

            migrationBuilder.RenameColumn(
                name: "Categoria_Id",
                table: "Categorias",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "tbl_Articulo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
