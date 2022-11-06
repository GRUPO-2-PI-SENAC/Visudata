using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PI.Infra.Data.Migrations
{
    public partial class alterEnterpriseCategoryAndDeleteMachineCategoryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Machine_category_CategoryId",
                table: "Machines");

            migrationBuilder.DropTable(
                name: "EnterpriseMachineCategory");

            migrationBuilder.DropTable(
                name: "Machine_category");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Machines",
                newName: "EnterpriseMachineCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Machines_CategoryId",
                table: "Machines",
                newName: "IX_Machines_EnterpriseMachineCategoryId");

            migrationBuilder.CreateTable(
                name: "EnterpriseMachienCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseMachienCategories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EnterpriseEnterpriseMachineCategory",
                columns: table => new
                {
                    EnterpriseId = table.Column<int>(type: "int", nullable: false),
                    EnterpriseMachineCategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseEnterpriseMachineCategory", x => new { x.EnterpriseId, x.EnterpriseMachineCategoriesId });
                    table.ForeignKey(
                        name: "FK_EnterpriseEnterpriseMachineCategory_EnterpriseMachienCategor~",
                        column: x => x.EnterpriseMachineCategoriesId,
                        principalTable: "EnterpriseMachienCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnterpriseEnterpriseMachineCategory_Enterprises_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseEnterpriseMachineCategory_EnterpriseMachineCategor~",
                table: "EnterpriseEnterpriseMachineCategory",
                column: "EnterpriseMachineCategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_EnterpriseMachienCategories_EnterpriseMachineCatego~",
                table: "Machines",
                column: "EnterpriseMachineCategoryId",
                principalTable: "EnterpriseMachienCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_EnterpriseMachienCategories_EnterpriseMachineCatego~",
                table: "Machines");

            migrationBuilder.DropTable(
                name: "EnterpriseEnterpriseMachineCategory");

            migrationBuilder.DropTable(
                name: "EnterpriseMachienCategories");

            migrationBuilder.RenameColumn(
                name: "EnterpriseMachineCategoryId",
                table: "Machines",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Machines_EnterpriseMachineCategoryId",
                table: "Machines",
                newName: "IX_Machines_CategoryId");

            migrationBuilder.CreateTable(
                name: "Machine_category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machine_category", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EnterpriseMachineCategory",
                columns: table => new
                {
                    EnterprisesId = table.Column<int>(type: "int", nullable: false),
                    MachineCategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseMachineCategory", x => new { x.EnterprisesId, x.MachineCategoriesId });
                    table.ForeignKey(
                        name: "FK_EnterpriseMachineCategory_Enterprises_EnterprisesId",
                        column: x => x.EnterprisesId,
                        principalTable: "Enterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnterpriseMachineCategory_Machine_category_MachineCategories~",
                        column: x => x.MachineCategoriesId,
                        principalTable: "Machine_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseMachineCategory_MachineCategoriesId",
                table: "EnterpriseMachineCategory",
                column: "MachineCategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Machine_category_CategoryId",
                table: "Machines",
                column: "CategoryId",
                principalTable: "Machine_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
