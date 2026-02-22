using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeaManagement.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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

            migrationBuilder.EnsureSchema(
                name: "stakeholder");

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
                name: "product_category",
                schema: "inventory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    parent_category_id = table.Column<int>(type: "integer", nullable: true),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tea_class",
                schema: "general_setup",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tea_class", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transaction_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    voucher_no = table.Column<string>(type: "text", nullable: false),
                    voucher_type_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
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
                name: "unit",
                schema: "inventory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    unit_name = table.Column<string>(type: "text", nullable: false),
                    unit_description = table.Column<string>(type: "text", nullable: true),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unit", x => x.id);
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
                name: "stakeholders",
                schema: "stakeholder",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stakeholder_type = table.Column<int>(type: "integer", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    stakeholder_code = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    ledger_id = table.Column<int>(type: "integer", nullable: false),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stakeholders", x => x.id);
                    table.ForeignKey(
                        name: "fk_stakeholders_ledger_ledger_id",
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
                name: "product",
                schema: "inventory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    unit_id = table.Column<int>(type: "integer", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_product_category_category_id",
                        column: x => x.category_id,
                        principalSchema: "inventory",
                        principalTable: "product_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_unit_unit_id",
                        column: x => x.unit_id,
                        principalSchema: "inventory",
                        principalTable: "unit",
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
                    factory_id = table.Column<int>(type: "integer", nullable: false),
                    txn_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    sale_no = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    bill_no = table.Column<string>(type: "text", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "payable",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stakeholder_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    txn_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    payment_id = table.Column<int>(type: "integer", nullable: true),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payable", x => x.id);
                    table.ForeignKey(
                        name: "fk_payable_stakeholders_stakeholder_id",
                        column: x => x.stakeholder_id,
                        principalSchema: "stakeholder",
                        principalTable: "stakeholders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "receivable",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stakeholder_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    txn_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    transaction_id = table.Column<int>(type: "integer", nullable: false),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_receivable", x => x.id);
                    table.ForeignKey(
                        name: "fk_receivable_stakeholders_stakeholder_id",
                        column: x => x.stakeholder_id,
                        principalSchema: "stakeholder",
                        principalTable: "stakeholders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_receivable_transactions_transaction_id",
                        column: x => x.transaction_id,
                        principalSchema: "accounting",
                        principalTable: "transactions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales_price",
                schema: "inventory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    class_id = table.Column<int>(type: "integer", nullable: true),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sales_price", x => x.id);
                    table.ForeignKey(
                        name: "fk_sales_price_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "inventory",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sale_details",
                schema: "inventory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sale_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    unit_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    rate = table.Column<decimal>(type: "numeric", nullable: false),
                    water_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    net_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    gross_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    net_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    rec_status = table.Column<char>(type: "character(1)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rec_by_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sale_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_sale_details_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "inventory",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sale_details_sales_sale_id",
                        column: x => x.sale_id,
                        principalSchema: "inventory",
                        principalTable: "sales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sale_details_unit_unit_id",
                        column: x => x.unit_id,
                        principalSchema: "inventory",
                        principalTable: "unit",
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
                name: "ix_payable_stakeholder_id",
                schema: "accounting",
                table: "payable",
                column: "stakeholder_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_category_id",
                schema: "inventory",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_unit_id",
                schema: "inventory",
                table: "product",
                column: "unit_id");

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

            migrationBuilder.CreateIndex(
                name: "ix_receivable_stakeholder_id",
                schema: "accounting",
                table: "receivable",
                column: "stakeholder_id");

            migrationBuilder.CreateIndex(
                name: "ix_receivable_transaction_id",
                schema: "accounting",
                table: "receivable",
                column: "transaction_id");

            migrationBuilder.CreateIndex(
                name: "ix_sale_details_product_id",
                schema: "inventory",
                table: "sale_details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_sale_details_sale_id",
                schema: "inventory",
                table: "sale_details",
                column: "sale_id");

            migrationBuilder.CreateIndex(
                name: "ix_sale_details_unit_id",
                schema: "inventory",
                table: "sale_details",
                column: "unit_id");

            migrationBuilder.CreateIndex(
                name: "ix_sales_factory_id",
                schema: "inventory",
                table: "sales",
                column: "factory_id");

            migrationBuilder.CreateIndex(
                name: "ix_sales_price_product_id",
                schema: "inventory",
                table: "sales_price",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_stakeholders_ledger_id",
                schema: "stakeholder",
                table: "stakeholders",
                column: "ledger_id");

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
                name: "payable",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "purchase_details",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "receivable",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "sale_details",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "sales_price",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "tea_class",
                schema: "general_setup");

            migrationBuilder.DropTable(
                name: "transaction_details",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "purchase",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "sales",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "product",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "transactions",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "stakeholders",
                schema: "stakeholder");

            migrationBuilder.DropTable(
                name: "factory",
                schema: "general_setup");

            migrationBuilder.DropTable(
                name: "product_category",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "unit",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "ledger",
                schema: "accounting");
        }
    }
}
