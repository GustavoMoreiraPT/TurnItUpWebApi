using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Repository.Migrations
{
    public partial class ClearGroupAndLanguageAndCustomerCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_CountryGroup_CountryGroupId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageGenres_GenrerGroup_GenrerGroupId",
                table: "LanguageGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageRoles_RoleGroup_RoleGroupId",
                table: "LanguageRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Countries_CountryId",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleGroup",
                table: "RoleGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenrerGroup",
                table: "GenrerGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryGroup",
                table: "CountryGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_CountryGroupId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "CustomerRoles");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CustomerRoles");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "CustomerGenres");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CustomerGenres");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "RoleGroup",
                newName: "RoleGroups");

            migrationBuilder.RenameTable(
                name: "GenrerGroup",
                newName: "GenrerGroups");

            migrationBuilder.RenameTable(
                name: "CountryGroup",
                newName: "CountryGroups");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "CustomerCountries");

            migrationBuilder.RenameColumn(
                name: "CountryGroupId",
                table: "CustomerCountries",
                newName: "GroupId");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "CustomerRoles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "CustomerGenres",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "CustomerCountries",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleGroups",
                table: "RoleGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenrerGroups",
                table: "GenrerGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryGroups",
                table: "CountryGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerCountries",
                table: "CustomerCountries",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LanguageCountries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryGroupId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageCountries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageCountries_CountryGroups_CountryGroupId",
                        column: x => x.CountryGroupId,
                        principalTable: "CountryGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCountries_CustomerId",
                table: "CustomerCountries",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageCountries_CountryGroupId",
                table: "LanguageCountries",
                column: "CountryGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_CustomerCountries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "CustomerCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCountries_Customers_CustomerId",
                table: "CustomerCountries",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageGenres_GenrerGroups_GenrerGroupId",
                table: "LanguageGenres",
                column: "GenrerGroupId",
                principalTable: "GenrerGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageRoles_RoleGroups_RoleGroupId",
                table: "LanguageRoles",
                column: "RoleGroupId",
                principalTable: "RoleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_CustomerCountries_CountryId",
                table: "Locations",
                column: "CountryId",
                principalTable: "CustomerCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_CustomerCountries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCountries_Customers_CustomerId",
                table: "CustomerCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageGenres_GenrerGroups_GenrerGroupId",
                table: "LanguageGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageRoles_RoleGroups_RoleGroupId",
                table: "LanguageRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_CustomerCountries_CountryId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "LanguageCountries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleGroups",
                table: "RoleGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenrerGroups",
                table: "GenrerGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerCountries",
                table: "CustomerCountries");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCountries_CustomerId",
                table: "CustomerCountries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryGroups",
                table: "CountryGroups");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "CustomerRoles");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "CustomerGenres");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerCountries");

            migrationBuilder.RenameTable(
                name: "RoleGroups",
                newName: "RoleGroup");

            migrationBuilder.RenameTable(
                name: "GenrerGroups",
                newName: "GenrerGroup");

            migrationBuilder.RenameTable(
                name: "CustomerCountries",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "CountryGroups",
                newName: "CountryGroup");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Countries",
                newName: "CountryGroupId");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "CustomerRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CustomerRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "CustomerGenres",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CustomerGenres",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleGroup",
                table: "RoleGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenrerGroup",
                table: "GenrerGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryGroup",
                table: "CountryGroup",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountryGroupId",
                table: "Countries",
                column: "CountryGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_CountryGroup_CountryGroupId",
                table: "Countries",
                column: "CountryGroupId",
                principalTable: "CountryGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageGenres_GenrerGroup_GenrerGroupId",
                table: "LanguageGenres",
                column: "GenrerGroupId",
                principalTable: "GenrerGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageRoles_RoleGroup_RoleGroupId",
                table: "LanguageRoles",
                column: "RoleGroupId",
                principalTable: "RoleGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Countries_CountryId",
                table: "Locations",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
