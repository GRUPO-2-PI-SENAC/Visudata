using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PI.Infra.Data.Migrations
{
    public partial class AlterEntitiesProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Machine_Category_EnterpriseMachineCategoryId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "RepresentativoOfEnterpriseAddress",
                table: "UserSupports",
                newName: "AddressEmailOfRepresentativeEmployee");

            migrationBuilder.RenameColumn(
                name: "EnterpriseMachineCategoryId",
                table: "Machines",
                newName: "MachineCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Machines_EnterpriseMachineCategoryId",
                table: "Machines",
                newName: "IX_Machines_MachineCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Machine_Category_MachineCategoryId",
                table: "Machines",
                column: "MachineCategoryId",
                principalTable: "Machine_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Machine_Category_MachineCategoryId",
                table: "Machines");

            migrationBuilder.RenameColumn(
                name: "AddressEmailOfRepresentativeEmployee",
                table: "UserSupports",
                newName: "RepresentativoOfEnterpriseAddress");

            migrationBuilder.RenameColumn(
                name: "MachineCategoryId",
                table: "Machines",
                newName: "EnterpriseMachineCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Machines_MachineCategoryId",
                table: "Machines",
                newName: "IX_Machines_EnterpriseMachineCategoryId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "Logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Machine_Category_EnterpriseMachineCategoryId",
                table: "Machines",
                column: "EnterpriseMachineCategoryId",
                principalTable: "Machine_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
