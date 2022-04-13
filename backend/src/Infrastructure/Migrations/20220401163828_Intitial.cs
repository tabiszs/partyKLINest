using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PartyKlinest.Infrastructure.Migrations
{
    public partial class Intitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cleaners",
                columns: table => new
                {
                    cleaner_id = table.Column<string>(type: "text", nullable: false),
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
                    client_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.client_id);
                });

            migrationBuilder.CreateTable(
                name: "schedule_entries",
                columns: table => new
                {
                    schedule_entry_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    end = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    day_of_week = table.Column<int>(type: "integer", nullable: false),
                    cleaner_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schedule_entries", x => x.schedule_entry_id);
                    table.ForeignKey(
                        name: "fk_schedule_entries_cleaners_cleaner_id",
                        column: x => x.cleaner_id,
                        principalTable: "cleaners",
                        principalColumn: "cleaner_id");
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
                    client_id = table.Column<string>(type: "text", nullable: false),
                    cleaner_id = table.Column<string>(type: "text", nullable: true),
                    address_street = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    address_building_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    address_flat_number = table.Column<string>(type: "text", nullable: true),
                    address_city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    address_postal_code = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: false),
                    address_country = table.Column<string>(type: "character varying(90)", maxLength: 90, nullable: false),
                    clients_opinion_rating = table.Column<int>(type: "integer", nullable: true),
                    clients_opinion_additional_info = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    cleaners_opinion_rating = table.Column<int>(type: "integer", nullable: true),
                    cleaners_opinion_additional_info = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.order_id);
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
                });

            migrationBuilder.CreateIndex(
                name: "ix_orders_cleaner_id",
                table: "orders",
                column: "cleaner_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_client_id",
                table: "orders",
                column: "client_id");

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
                name: "clients");

            migrationBuilder.DropTable(
                name: "cleaners");
        }
    }
}
