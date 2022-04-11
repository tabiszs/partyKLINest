using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyKlinest.Infrastructure.Migrations
{
    public partial class AddDecimalKeyValuePairs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "decimal_key_value_pairs",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_decimal_key_value_pairs", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "decimal_key_value_pairs");
        }
    }
}
