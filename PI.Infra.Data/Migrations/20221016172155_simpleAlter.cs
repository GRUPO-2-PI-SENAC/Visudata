using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PI.Infra.Data.Migrations
{
    public partial class simpleAlter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enterprises_EnterpriseStatus_EnterpriseStatusId",
                table: "Enterprises");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Category_CategoryId",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_MachineStatus_StatusId",
                table: "Machines");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MachineStatus",
                table: "MachineStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnterpriseStatus",
                table: "EnterpriseStatus");

            migrationBuilder.RenameTable(
                name: "MachineStatus",
                newName: "Machine_status");

            migrationBuilder.RenameTable(
                name: "EnterpriseStatus",
                newName: "Enterprise_status");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Enterprises",
                newName: "SocialReason");

            migrationBuilder.AddColumn<string>(
                name: "FantasyName",
                table: "Enterprises",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NumberOfLocation",
                table: "Enterprises",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Machine_status",
                table: "Machine_status",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enterprise_status",
                table: "Enterprise_status",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Machine_category",
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
                    table.PrimaryKey("PK_Machine_category", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "us_problems_category",
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
                    table.PrimaryKey("PK_us_problems_category", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "UserSupports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EnterpriseId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RepresentativoOfEnterpriseAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProblemsCategoryId = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSupports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSupports_Enterprises_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSupports_us_problems_category_ProblemsCategoryId",
                        column: x => x.ProblemsCategoryId,
                        principalTable: "us_problems_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseMachineCategory_MachineCategoriesId",
                table: "EnterpriseMachineCategory",
                column: "MachineCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSupports_EnterpriseId",
                table: "UserSupports",
                column: "EnterpriseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSupports_ProblemsCategoryId",
                table: "UserSupports",
                column: "ProblemsCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enterprises_Enterprise_status_EnterpriseStatusId",
                table: "Enterprises",
                column: "EnterpriseStatusId",
                principalTable: "Enterprise_status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Machine_category_CategoryId",
                table: "Machines",
                column: "CategoryId",
                principalTable: "Machine_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Machine_status_StatusId",
                table: "Machines",
                column: "StatusId",
                principalTable: "Machine_status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enterprises_Enterprise_status_EnterpriseStatusId",
                table: "Enterprises");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Machine_category_CategoryId",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Machine_status_StatusId",
                table: "Machines");

            migrationBuilder.DropTable(
                name: "EnterpriseMachineCategory");

            migrationBuilder.DropTable(
                name: "UserSupports");

            migrationBuilder.DropTable(
                name: "Machine_category");

            migrationBuilder.DropTable(
                name: "us_problems_category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Machine_status",
                table: "Machine_status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enterprise_status",
                table: "Enterprise_status");

            migrationBuilder.DropColumn(
                name: "FantasyName",
                table: "Enterprises");

            migrationBuilder.DropColumn(
                name: "NumberOfLocation",
                table: "Enterprises");

            migrationBuilder.RenameTable(
                name: "Machine_status",
                newName: "MachineStatus");

            migrationBuilder.RenameTable(
                name: "Enterprise_status",
                newName: "EnterpriseStatus");

            migrationBuilder.RenameColumn(
                name: "SocialReason",
                table: "Enterprises",
                newName: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MachineStatus",
                table: "MachineStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnterpriseStatus",
                table: "EnterpriseStatus",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Category",
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
                    table.PrimaryKey("PK_Category", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Enterprises_EnterpriseStatus_EnterpriseStatusId",
                table: "Enterprises",
                column: "EnterpriseStatusId",
                principalTable: "EnterpriseStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Category_CategoryId",
                table: "Machines",
                column: "CategoryId",
                principalTable: "Category",
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
    }
}
