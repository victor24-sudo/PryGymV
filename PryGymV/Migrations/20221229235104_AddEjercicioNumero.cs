using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PryGymV.Migrations
{
    public partial class AddEjercicioNumero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Ejercicio",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Ejercicio");
        }
    }
}
