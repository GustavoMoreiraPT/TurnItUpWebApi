using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Repository.Migrations
{
    public partial class newSupports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleGroupId",
                table: "LanguageRoles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenrerGroupId",
                table: "LanguageGenres",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguageGroupId",
                table: "LanguageGenres",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryGroupId",
                table: "Countries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CountryGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DefaultName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenrerGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DefaultName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenrerGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DefaultName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanguageRoles_RoleGroupId",
                table: "LanguageRoles",
                column: "RoleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageGenres_GenrerGroupId",
                table: "LanguageGenres",
                column: "GenrerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountryGroupId",
                table: "Countries",
                column: "CountryGroupId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_CountryGroup_CountryGroupId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageGenres_GenrerGroup_GenrerGroupId",
                table: "LanguageGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageRoles_RoleGroup_RoleGroupId",
                table: "LanguageRoles");

            migrationBuilder.DropTable(
                name: "CountryGroup");

            migrationBuilder.DropTable(
                name: "GenrerGroup");

            migrationBuilder.DropTable(
                name: "RoleGroup");

            migrationBuilder.DropIndex(
                name: "IX_LanguageRoles_RoleGroupId",
                table: "LanguageRoles");

            migrationBuilder.DropIndex(
                name: "IX_LanguageGenres_GenrerGroupId",
                table: "LanguageGenres");

            migrationBuilder.DropIndex(
                name: "IX_Countries_CountryGroupId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "RoleGroupId",
                table: "LanguageRoles");

            migrationBuilder.DropColumn(
                name: "GenrerGroupId",
                table: "LanguageGenres");

            migrationBuilder.DropColumn(
                name: "LanguageGroupId",
                table: "LanguageGenres");

            migrationBuilder.DropColumn(
                name: "CountryGroupId",
                table: "Countries");
        }
    }
}
