using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeaManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "general_setup");

            migrationBuilder.EnsureSchema(
                name: "accounting");

            migrationBuilder.EnsureSchema(
                name: "inventory");

            migrationBuilder.CreateTable(
                name: "coa",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_coa", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ledger",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    parent_id = table.Column<int>(type: "integer", nullable: true),
                    sub_parent_id = table.Column<int>(type: "integer", nullable: true),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ledger", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "general_setup",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: true),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transaction_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    type_id = table.Column<int>(type: "integer", nullable: false),
                    is_reversed = table.Column<bool>(type: "boolean", nullable: false),
                    reversed_id = table.Column<int>(type: "integer", nullable: true),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transactions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "factory",
                schema: "general_setup",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: true),
                    contact_number = table.Column<string>(type: "text", nullable: true),
                    country = table.Column<string>(type: "text", nullable: false),
                    ledger_id = table.Column<int>(type: "integer", nullable: false),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_factory", x => x.id);
                    table.ForeignKey(
                        name: "fk_factory_ledger_ledger_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledger",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transaction_details",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transaction_id = table.Column<int>(type: "integer", nullable: false),
                    ledger_id = table.Column<int>(type: "integer", nullable: false),
                    dr_cr = table.Column<char>(type: "character(1)", nullable: false),
                    dr_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    cr_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_transaction_details_ledger_ledger_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledger",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_transaction_details_transactions_transaction_id",
                        column: x => x.transaction_id,
                        principalSchema: "accounting",
                        principalTable: "transactions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bonus",
                schema: "general_setup",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    factory_id = table.Column<int>(type: "integer", nullable: false),
                    bonus_per_kg = table.Column<decimal>(type: "numeric", nullable: false),
                    ledger_id = table.Column<int>(type: "integer", nullable: true),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bonus", x => x.id);
                    table.ForeignKey(
                        name: "fk_bonus_factory_factory_id",
                        column: x => x.factory_id,
                        principalSchema: "general_setup",
                        principalTable: "factory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales",
                schema: "inventory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    factory_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    water_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    net_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sales", x => x.id);
                    table.ForeignKey(
                        name: "fk_sales_factory_factory_id",
                        column: x => x.factory_id,
                        principalSchema: "general_setup",
                        principalTable: "factory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sales_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "general_setup",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_bonus_factory_id",
                schema: "general_setup",
                table: "bonus",
                column: "factory_id");

            migrationBuilder.CreateIndex(
                name: "ix_factory_ledger_id",
                schema: "general_setup",
                table: "factory",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "ix_sales_factory_id",
                schema: "inventory",
                table: "sales",
                column: "factory_id");

            migrationBuilder.CreateIndex(
                name: "ix_sales_product_id",
                schema: "inventory",
                table: "sales",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_details_ledger_id",
                schema: "accounting",
                table: "transaction_details",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_details_transaction_id",
                schema: "accounting",
                table: "transaction_details",
                column: "transaction_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bonus",
                schema: "general_setup");

            migrationBuilder.DropTable(
                name: "coa",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "sales",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "transaction_details",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "factory",
                schema: "general_setup");

            migrationBuilder.DropTable(
                name: "product",
                schema: "general_setup");

            migrationBuilder.DropTable(
                name: "transactions",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ledger",
                schema: "accounting");
        }
    }
}
