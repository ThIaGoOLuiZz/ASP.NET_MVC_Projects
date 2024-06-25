using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMController.Migrations.ContextoVMMigrations
{
    public partial class InicialAtualização : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemoriaRam",
                table: "VirtualMachines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemoriaRam",
                table: "VirtualMachines");
        }
    }
}
