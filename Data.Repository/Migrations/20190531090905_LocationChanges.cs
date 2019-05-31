using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Repository.Migrations
{
    public partial class LocationChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Cities_CityId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_CustomerCountries_CountryId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CityId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CountryId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryGroupId",
                table: "Locations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LocationId",
                table: "Customers",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Locations_LocationId",
                table: "Customers",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Locations_LocationId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_LocationId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CountryGroupId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CityId",
                table: "Locations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CountryId",
                table: "Locations",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Cities_CityId",
                table: "Locations",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_CustomerCountries_CountryId",
                table: "Locations",
                column: "CountryId",
                principalTable: "CustomerCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
