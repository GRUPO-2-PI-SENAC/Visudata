using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PI.Infra.Data.Migrations
{
    public partial class UpdateNameOfEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_EnterpriseMachienCategories_EnterpriseMachineCatego~",
                table: "Machines");

            migrationBuilder.DropTable(
                name: "EnterpriseMachienCategories");

            migrationBuilder.CreateTable(
                name: "Machine_Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnterpriseId = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machine_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machine_Category_Enterprises_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Machine_Category_EnterpriseId",
                table: "Machine_Category",
                column: "EnterpriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Machine_Category_EnterpriseMachineCategoryId",
                table: "Machines",
                column: "EnterpriseMachineCategoryId",
                principalTable: "Machine_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Machine_Category_EnterpriseMachineCategoryId",
                table: "Machines");

            migrationBuilder.DropTable(
                name: "Machine_Category");

            migrationBuilder.CreateTable(
                name: "EnterpriseMachienCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EnterpriseId = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseMachienCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnterpriseMachienCategories_Enterprises_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseMachienCategories_EnterpriseId",
                table: "EnterpriseMachienCategories",
                column: "EnterpriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_EnterpriseMachienCategories_EnterpriseMachineCatego~",
                table: "Machines",
                column: "EnterpriseMachineCategoryId",
                principalTable: "EnterpriseMachienCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
