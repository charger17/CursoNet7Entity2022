using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCoreDatabaseFirst.Migrations
{
    public partial class BorradoPais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "País",
                table: "usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "País",
                table: "usuario",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
