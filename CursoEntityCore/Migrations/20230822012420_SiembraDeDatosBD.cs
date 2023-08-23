using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoEntityCore.Migrations
{
    public partial class SiembraDeDatosBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Categoria_Id", "Activo", "FechaCreacion", "Nombre" },
                values: new object[] { 33, true, new DateTime(2023, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Categoria 5" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Categoria_Id", "Activo", "FechaCreacion", "Nombre" },
                values: new object[] { 34, false, new DateTime(2023, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Categoria 6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Categoria_Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Categoria_Id",
                keyValue: 34);
        }
    }
}
