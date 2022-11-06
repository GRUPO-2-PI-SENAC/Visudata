using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PI.Infra.Data.Migrations
{
    public partial class alterAttributesInMachineEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Machines",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Machines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnterpriseStatusId",
                table: "Enterprises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EnterpriseStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseStatus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MachineStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineStatus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_StatusId",
                table: "Machines",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Enterprises_EnterpriseStatusId",
                table: "Enterprises",
                column: "EnterpriseStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enterprises_EnterpriseStatus_EnterpriseStatusId",
                table: "Enterprises",
                column: "EnterpriseStatusId",
                principalTable: "EnterpriseStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_MachineStatus_StatusId",
                table: "Machines",
                column: "StatusId",
                principalTable: "MachineStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enterprises_EnterpriseStatus_EnterpriseStatusId",
                table: "Enterprises");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_MachineStatus_StatusId",
                table: "Machines");

            migrationBuilder.DropTable(
                name: "EnterpriseStatus");

            migrationBuilder.DropTable(
                name: "MachineStatus");

            migrationBuilder.DropIndex(
                name: "IX_Machines_StatusId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Enterprises_EnterpriseStatusId",
                table: "Enterprises");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "EnterpriseStatusId",
                table: "Enterprises");
        }
    }
}
