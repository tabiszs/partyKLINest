using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyKlinest.Infrastructure.Migrations
{
    public partial class CleanerOwnsOneOrderFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "min_price",
                table: "cleaners",
                newName: "order_filter_min_price");

            migrationBuilder.RenameColumn(
                name: "min_client_rating",
                table: "cleaners",
                newName: "order_filter_min_client_rating");

            migrationBuilder.RenameColumn(
                name: "max_mess_level",
                table: "cleaners",
                newName: "order_filter_max_mess_level");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "order_filter_min_price",
                table: "cleaners",
                newName: "min_price");

            migrationBuilder.RenameColumn(
                name: "order_filter_min_client_rating",
                table: "cleaners",
                newName: "min_client_rating");

            migrationBuilder.RenameColumn(
                name: "order_filter_max_mess_level",
                table: "cleaners",
                newName: "max_mess_level");
        }
    }
}
