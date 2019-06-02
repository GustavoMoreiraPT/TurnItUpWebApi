using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Repository.Migrations
{
    public partial class EventsAndReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventLocations_LocationId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Prices_PriceId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Ratings_RatingId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_CustomerRoles_RoleId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventState_StateId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventLocations");

            migrationBuilder.DropTable(
                name: "EventState");

            migrationBuilder.DropIndex(
                name: "IX_Events_PriceId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_RatingId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_RoleId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_StateId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Events",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Events",
                newName: "RoleGroupId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Events",
                newName: "EventManagerId");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Events",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "EventReviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReviewerId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    EventReviewPhotoId = table.Column<int>(nullable: true),
                    ReviewDate = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventReviews_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventReviews_Images_EventReviewPhotoId",
                        column: x => x.EventReviewPhotoId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventReviews_Customers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventReviews_EventId",
                table: "EventReviews",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventReviews_EventReviewPhotoId",
                table: "EventReviews",
                column: "EventReviewPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventReviews_ReviewerId",
                table: "EventReviews",
                column: "ReviewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventReviews");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "RoleGroupId",
                table: "Events",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "EventManagerId",
                table: "Events",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Events",
                newName: "StartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RatingId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LocationId = table.Column<int>(nullable: true),
                    StreetName = table.Column<string>(nullable: true),
                    StreetNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventState", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_PriceId",
                table: "Events",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_RatingId",
                table: "Events",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_RoleId",
                table: "Events",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_StateId",
                table: "Events",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_EventLocations_LocationId",
                table: "EventLocations",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventLocations_LocationId",
                table: "Events",
                column: "LocationId",
                principalTable: "EventLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Prices_PriceId",
                table: "Events",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Ratings_RatingId",
                table: "Events",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_CustomerRoles_RoleId",
                table: "Events",
                column: "RoleId",
                principalTable: "CustomerRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventState_StateId",
                table: "Events",
                column: "StateId",
                principalTable: "EventState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
