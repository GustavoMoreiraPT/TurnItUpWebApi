using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Repository.Migrations
{
    public partial class TrackComplementaryInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tracks",
                newName: "TrackName");

            migrationBuilder.AddColumn<string>(
                name: "ArtistName",
                table: "Tracks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Tracks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtistName",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Tracks");

            migrationBuilder.RenameColumn(
                name: "TrackName",
                table: "Tracks",
                newName: "Name");
        }
    }
}
