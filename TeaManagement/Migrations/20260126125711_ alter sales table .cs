using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaManagement.Migrations
{
    /// <inheritdoc />
    public partial class altersalestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "amount",
                schema: "accounting",
                table: "transactions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "transaction_id",
                schema: "inventory",
                table: "sales",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                schema: "general_setup",
                table: "product",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_sales_transaction_id",
                schema: "inventory",
                table: "sales",
                column: "transaction_id");

            migrationBuilder.AddForeignKey(
                name: "fk_sales_transactions_transaction_id",
                schema: "inventory",
                table: "sales",
                column: "transaction_id",
                principalSchema: "accounting",
                principalTable: "transactions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_sales_transactions_transaction_id",
                schema: "inventory",
                table: "sales");

            migrationBuilder.DropIndex(
                name: "ix_sales_transaction_id",
                schema: "inventory",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "amount",
                schema: "accounting",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "transaction_id",
                schema: "inventory",
                table: "sales");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                schema: "general_setup",
                table: "product",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
