using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Repository.Migrations
{
    public partial class modelbuilder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerRoles_Customers_CustomerId",
                table: "CustomerRoles");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "CustomerRoles",
                newName: "customerId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerRoles_CustomerId",
                table: "CustomerRoles",
                newName: "IX_CustomerRoles_customerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerRoles_Customers_customerId",
                table: "CustomerRoles",
                column: "customerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerRoles_Customers_customerId",
                table: "CustomerRoles");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "CustomerRoles",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerRoles_customerId",
                table: "CustomerRoles",
                newName: "IX_CustomerRoles_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerRoles_Customers_CustomerId",
                table: "CustomerRoles",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
