using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMController.Migrations.ProcessosRPA
{
    public partial class Inicialcriacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VirtualMachine",
                columns: table => new
                {
                    idVM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemoriaRam = table.Column<int>(type: "int", nullable: false),
                    CapacidadeHoras = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualMachine", x => x.idVM);
                });

            migrationBuilder.CreateTable(
                name: "ProcessosRPA",
                columns: table => new
                {
                    IdProcesso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProcesso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusProcesso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdVM = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessosRPA", x => x.IdProcesso);
                    table.ForeignKey(
                        name: "FK_ProcessosRPA_VirtualMachine_IdVM",
                        column: x => x.IdVM,
                        principalTable: "VirtualMachine",
                        principalColumn: "idVM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessosRPA_IdVM",
                table: "ProcessosRPA",
                column: "IdVM");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessosRPA");

            migrationBuilder.DropTable(
                name: "VirtualMachine");
        }
    }
}
