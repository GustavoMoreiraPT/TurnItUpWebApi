using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Repository.Migrations
{
    public partial class LanguageSuports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Role_RoleId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Genders_Customers_CustomerId",
                table: "Genders");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Customers_CustomerId",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genders",
                table: "Genders");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "CustomerRoles");

            migrationBuilder.RenameTable(
                name: "Genders",
                newName: "CustomerGenres");

            migrationBuilder.RenameIndex(
                name: "IX_Role_CustomerId",
                table: "CustomerRoles",
                newName: "IX_CustomerRoles_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Genders_CustomerId",
                table: "CustomerGenres",
                newName: "IX_CustomerGenres_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerRoles",
                table: "CustomerRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerGenres",
                table: "CustomerGenres",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LanguageGenres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageGenres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageRoles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerGenres_Customers_CustomerId",
                table: "CustomerGenres",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerRoles_Customers_CustomerId",
                table: "CustomerRoles",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_CustomerRoles_RoleId",
                table: "Events",
                column: "RoleId",
                principalTable: "CustomerRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerGenres_Customers_CustomerId",
                table: "CustomerGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerRoles_Customers_CustomerId",
                table: "CustomerRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_CustomerRoles_RoleId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "LanguageGenres");

            migrationBuilder.DropTable(
                name: "LanguageRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerRoles",
                table: "CustomerRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerGenres",
                table: "CustomerGenres");

            migrationBuilder.RenameTable(
                name: "CustomerRoles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "CustomerGenres",
                newName: "Genders");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerRoles_CustomerId",
                table: "Role",
                newName: "IX_Role_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerGenres_CustomerId",
                table: "Genders",
                newName: "IX_Genders_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genders",
                table: "Genders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Role_RoleId",
                table: "Events",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Genders_Customers_CustomerId",
                table: "Genders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Customers_CustomerId",
                table: "Role",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
