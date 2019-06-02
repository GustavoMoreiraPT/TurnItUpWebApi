using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Repository.Migrations
{
    public partial class makedecimalnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
