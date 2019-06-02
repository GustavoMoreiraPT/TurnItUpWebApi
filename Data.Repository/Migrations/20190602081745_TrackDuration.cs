using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Repository.Migrations
{
    public partial class TrackDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationInSeconds",
                table: "Tracks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInSeconds",
                table: "Tracks");
        }
    }
}
