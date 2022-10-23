using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PI.Infra.Data.Migrations
{
    public partial class AlterRelationshipBetweenenterpriseAndEnterpriseMachineCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnterpriseEnterpriseMachineCategory");

            migrationBuilder.AddColumn<int>(
                name: "EnterpriseId",
                table: "EnterpriseMachienCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseMachienCategories_EnterpriseId",
                table: "EnterpriseMachienCategories",
                column: "EnterpriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnterpriseMachienCategories_Enterprises_EnterpriseId",
                table: "EnterpriseMachienCategories",
                column: "EnterpriseId",
                principalTable: "Enterprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnterpriseMachienCategories_Enterprises_EnterpriseId",
                table: "EnterpriseMachienCategories");

            migrationBuilder.DropIndex(
                name: "IX_EnterpriseMachienCategories_EnterpriseId",
                table: "EnterpriseMachienCategories");

            migrationBuilder.DropColumn(
                name: "EnterpriseId",
                table: "EnterpriseMachienCategories");

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
        }
    }
}
