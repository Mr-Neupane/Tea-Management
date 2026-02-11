using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeaManagement.Migrations
{
    /// <inheritdoc />
    public partial class Purchasetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "purchase",
                schema: "inventory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    supplier_id = table.Column<int>(type: "integer", nullable: false),
                    purchase_no = table.Column<string>(type: "text", nullable: false),
                    bill_no = table.Column<string>(type: "text", nullable: true),
                    gross_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    discount = table.Column<decimal>(type: "numeric", nullable: false),
                    net_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    txn_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchase", x => x.id);
                    table.ForeignKey(
                        name: "fk_purchase_stakeholders_supplier_id",
                        column: x => x.supplier_id,
                        principalSchema: "stakeholder",
                        principalTable: "stakeholders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase_details",
                schema: "inventory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    purchase_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    unit_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    rate = table.Column<decimal>(type: "numeric", nullable: false),
                    discount = table.Column<decimal>(type: "numeric", nullable: false),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchase_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_purchase_details_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "inventory",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_purchase_details_purchase_purchase_id",
                        column: x => x.purchase_id,
                        principalSchema: "inventory",
                        principalTable: "purchase",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_purchase_details_unit_unit_id",
                        column: x => x.unit_id,
                        principalSchema: "inventory",
                        principalTable: "unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_purchase_supplier_id",
                schema: "inventory",
                table: "purchase",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_details_product_id",
                schema: "inventory",
                table: "purchase_details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_details_purchase_id",
                schema: "inventory",
                table: "purchase_details",
                column: "purchase_id");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_details_unit_id",
                schema: "inventory",
                table: "purchase_details",
                column: "unit_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "purchase_details",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "purchase",
                schema: "inventory");
        }
    }
}
