using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PryGymV.Migrations
{
    public partial class PryGymV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Recomendado",
                table: "Ejercicio",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recomendado",
                table: "Ejercicio");
        }
    }
}
