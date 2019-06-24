using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Repository.Migrations
{
    public partial class TrackLocationSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackAudioLocation",
                table: "Tracks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrackPhotoLocation",
                table: "Tracks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackAudioLocation",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "TrackPhotoLocation",
                table: "Tracks");
        }
    }
}
