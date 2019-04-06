using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Repository.Migrations
{
    public partial class offers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genders_TurnItUpUsers_MusicianId",
                table: "Genders");

            migrationBuilder.RenameColumn(
                name: "MusicianId",
                table: "Genders",
                newName: "TurnItUpUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Genders_MusicianId",
                table: "Genders",
                newName: "IX_Genders_TurnItUpUserId");

            migrationBuilder.AddColumn<int>(
                name: "ReviewsCount",
                table: "TurnItUpUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecruiterId",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_RecruiterId",
                table: "Events",
                column: "RecruiterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_TurnItUpUsers_RecruiterId",
                table: "Events",
                column: "RecruiterId",
                principalTable: "TurnItUpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Genders_TurnItUpUsers_TurnItUpUserId",
                table: "Genders",
                column: "TurnItUpUserId",
                principalTable: "TurnItUpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_TurnItUpUsers_RecruiterId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Genders_TurnItUpUsers_TurnItUpUserId",
                table: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_Events_RecruiterId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ReviewsCount",
                table: "TurnItUpUsers");

            migrationBuilder.DropColumn(
                name: "RecruiterId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "TurnItUpUserId",
                table: "Genders",
                newName: "MusicianId");

            migrationBuilder.RenameIndex(
                name: "IX_Genders_TurnItUpUserId",
                table: "Genders",
                newName: "IX_Genders_MusicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genders_TurnItUpUsers_MusicianId",
                table: "Genders",
                column: "MusicianId",
                principalTable: "TurnItUpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
