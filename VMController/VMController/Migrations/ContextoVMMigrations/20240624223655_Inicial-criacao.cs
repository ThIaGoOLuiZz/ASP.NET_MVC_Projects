using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMController.Migrations.ContextoVMMigrations
{
    public partial class Inicialcriacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VirtualMachines",
                columns: table => new
                {
                    idVM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapacidadeHoras = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualMachines", x => x.idVM);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VirtualMachines");
        }
    }
}
