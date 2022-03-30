using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PartyKlinest.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    address_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    street = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    building_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    flat_number = table.Column<int>(type: "integer", nullable: true),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    postal_code = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: false),
                    country = table.Column<string>(type: "character varying(90)", maxLength: 90, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.address_id);
                });

            migrationBuilder.CreateTable(
                name: "cleaners",
                columns: table => new
                {
                    cleaner_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<int>(type: "integer", nullable: false),
                    max_mess_level = table.Column<int>(type: "integer", nullable: false),
                    min_price = table.Column<decimal>(type: "money", nullable: false),
                    min_client_rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cleaners", x => x.cleaner_id);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    client_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.client_id);
                });

            migrationBuilder.CreateTable(
                name: "opinions",
                columns: table => new
                {
                    opinion_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    additional_info = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_opinions", x => x.opinion_id);
                });

            migrationBuilder.CreateTable(
                name: "schedule_entries",
                columns: table => new
                {
                    schedule_entry_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cleaner_id = table.Column<long>(type: "bigint", nullable: false),
                    start = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    end = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    day_of_week = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schedule_entries", x => x.schedule_entry_id);
                    table.ForeignKey(
                        name: "fk_schedule_entries_cleaners_cleaner_id",
                        column: x => x.cleaner_id,
                        principalTable: "cleaners",
                        principalColumn: "cleaner_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    max_price = table.Column<decimal>(type: "money", nullable: false),
                    min_cleaner_rating = table.Column<int>(type: "integer", nullable: false),
                    mess_level = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    client_id = table.Column<long>(type: "bigint", nullable: false),
                    cleaner_id = table.Column<long>(type: "bigint", nullable: true),
                    address_id = table.Column<long>(type: "bigint", nullable: false),
                    clients_opinion_id = table.Column<long>(type: "bigint", nullable: true),
                    cleaners_opinion_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.order_id);
                    table.ForeignKey(
                        name: "fk_orders_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "address_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_cleaners_cleaner_id",
                        column: x => x.cleaner_id,
                        principalTable: "cleaners",
                        principalColumn: "cleaner_id");
                    table.ForeignKey(
                        name: "fk_orders_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "client_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_opinions_cleaners_opinion_id",
                        column: x => x.cleaners_opinion_id,
                        principalTable: "opinions",
                        principalColumn: "opinion_id");
                    table.ForeignKey(
                        name: "fk_orders_opinions_clients_opinion_id",
                        column: x => x.clients_opinion_id,
                        principalTable: "opinions",
                        principalColumn: "opinion_id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_orders_address_id",
                table: "orders",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_cleaner_id",
                table: "orders",
                column: "cleaner_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_cleaners_opinion_id",
                table: "orders",
                column: "cleaners_opinion_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_client_id",
                table: "orders",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_clients_opinion_id",
                table: "orders",
                column: "clients_opinion_id");

            migrationBuilder.CreateIndex(
                name: "ix_schedule_entries_cleaner_id",
                table: "schedule_entries",
                column: "cleaner_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "schedule_entries");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "opinions");

            migrationBuilder.DropTable(
                name: "cleaners");
        }
    }
}
