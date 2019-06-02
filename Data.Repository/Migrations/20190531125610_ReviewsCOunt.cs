using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Repository.Migrations
{
    public partial class ReviewsCOunt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewsCount",
                table: "Customers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewsCount",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(decimal));
        }
    }
}
