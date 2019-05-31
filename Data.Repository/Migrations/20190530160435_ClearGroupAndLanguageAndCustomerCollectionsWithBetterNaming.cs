using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Repository.Migrations
{
    public partial class ClearGroupAndLanguageAndCustomerCollectionsWithBetterNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanguageCountries_CountryGroups_CountryGroupId",
                table: "LanguageCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageGenres_GenrerGroups_GenrerGroupId",
                table: "LanguageGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageRoles_RoleGroups_RoleGroupId",
                table: "LanguageRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleGroups",
                table: "RoleGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenrerGroups",
                table: "GenrerGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryGroups",
                table: "CountryGroups");

            migrationBuilder.RenameTable(
                name: "RoleGroups",
                newName: "GroupsOfRoles");

            migrationBuilder.RenameTable(
                name: "GenrerGroups",
                newName: "GroupsOfGenrers");

            migrationBuilder.RenameTable(
                name: "CountryGroups",
                newName: "GroupsOfCountries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupsOfRoles",
                table: "GroupsOfRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupsOfGenrers",
                table: "GroupsOfGenrers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupsOfCountries",
                table: "GroupsOfCountries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageCountries_GroupsOfCountries_CountryGroupId",
                table: "LanguageCountries",
                column: "CountryGroupId",
                principalTable: "GroupsOfCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageGenres_GroupsOfGenrers_GenrerGroupId",
                table: "LanguageGenres",
                column: "GenrerGroupId",
                principalTable: "GroupsOfGenrers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageRoles_GroupsOfRoles_RoleGroupId",
                table: "LanguageRoles",
                column: "RoleGroupId",
                principalTable: "GroupsOfRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanguageCountries_GroupsOfCountries_CountryGroupId",
                table: "LanguageCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageGenres_GroupsOfGenrers_GenrerGroupId",
                table: "LanguageGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageRoles_GroupsOfRoles_RoleGroupId",
                table: "LanguageRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupsOfRoles",
                table: "GroupsOfRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupsOfGenrers",
                table: "GroupsOfGenrers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupsOfCountries",
                table: "GroupsOfCountries");

            migrationBuilder.RenameTable(
                name: "GroupsOfRoles",
                newName: "RoleGroups");

            migrationBuilder.RenameTable(
                name: "GroupsOfGenrers",
                newName: "GenrerGroups");

            migrationBuilder.RenameTable(
                name: "GroupsOfCountries",
                newName: "CountryGroups");

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

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageCountries_CountryGroups_CountryGroupId",
                table: "LanguageCountries",
                column: "CountryGroupId",
                principalTable: "CountryGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
        }
    }
}
