using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Repository.Migrations
{
    public partial class PhotoToTracks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackPhotoId",
                table: "Tracks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_TrackPhotoId",
                table: "Tracks",
                column: "TrackPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Images_TrackPhotoId",
                table: "Tracks",
                column: "TrackPhotoId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Images_TrackPhotoId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_TrackPhotoId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "TrackPhotoId",
                table: "Tracks");
        }
    }
}
