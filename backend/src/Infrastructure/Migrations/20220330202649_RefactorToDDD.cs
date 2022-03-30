using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PartyKlinest.Infrastructure.Migrations
{
    public partial class RefactorToDDD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_addresses_address_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_opinions_cleaners_opinion_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_opinions_clients_opinion_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "fk_schedule_entries_cleaners_cleaner_id",
                table: "schedule_entries");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "opinions");

            migrationBuilder.DropIndex(
                name: "ix_orders_address_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_cleaners_opinion_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_clients_opinion_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "address_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "cleaners_opinion_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "clients_opinion_id",
                table: "orders");

            migrationBuilder.AlterColumn<long>(
                name: "cleaner_id",
                table: "schedule_entries",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "address_building_number",
                table: "orders",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address_city",
                table: "orders",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address_country",
                table: "orders",
                type: "character varying(90)",
                maxLength: 90,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address_flat_number",
                table: "orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address_postal_code",
                table: "orders",
                type: "character varying(18)",
                maxLength: 18,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address_street",
                table: "orders",
                type: "character varying(180)",
                maxLength: 180,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cleaners_opinion_additional_info",
                table: "orders",
                type: "character varying(4096)",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cleaners_opinion_rating",
                table: "orders",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "clients_opinion_additional_info",
                table: "orders",
                type: "character varying(4096)",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "clients_opinion_rating",
                table: "orders",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_schedule_entries_cleaners_cleaner_id",
                table: "schedule_entries",
                column: "cleaner_id",
                principalTable: "cleaners",
                principalColumn: "cleaner_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_schedule_entries_cleaners_cleaner_id",
                table: "schedule_entries");

            migrationBuilder.DropColumn(
                name: "address_building_number",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "address_city",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "address_country",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "address_flat_number",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "address_postal_code",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "address_street",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "cleaners_opinion_additional_info",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "cleaners_opinion_rating",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "clients_opinion_additional_info",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "clients_opinion_rating",
                table: "orders");

            migrationBuilder.AlterColumn<long>(
                name: "cleaner_id",
                table: "schedule_entries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "address_id",
                table: "orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "cleaners_opinion_id",
                table: "orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "clients_opinion_id",
                table: "orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    address_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    building_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    country = table.Column<string>(type: "character varying(90)", maxLength: 90, nullable: false),
                    flat_number = table.Column<int>(type: "integer", nullable: true),
                    postal_code = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: false),
                    street = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.address_id);
                });

            migrationBuilder.CreateTable(
                name: "opinions",
                columns: table => new
                {
                    opinion_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    additional_info = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_opinions", x => x.opinion_id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_orders_address_id",
                table: "orders",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_cleaners_opinion_id",
                table: "orders",
                column: "cleaners_opinion_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_clients_opinion_id",
                table: "orders",
                column: "clients_opinion_id");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_addresses_address_id",
                table: "orders",
                column: "address_id",
                principalTable: "addresses",
                principalColumn: "address_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_opinions_cleaners_opinion_id",
                table: "orders",
                column: "cleaners_opinion_id",
                principalTable: "opinions",
                principalColumn: "opinion_id");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_opinions_clients_opinion_id",
                table: "orders",
                column: "clients_opinion_id",
                principalTable: "opinions",
                principalColumn: "opinion_id");

            migrationBuilder.AddForeignKey(
                name: "fk_schedule_entries_cleaners_cleaner_id",
                table: "schedule_entries",
                column: "cleaner_id",
                principalTable: "cleaners",
                principalColumn: "cleaner_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
