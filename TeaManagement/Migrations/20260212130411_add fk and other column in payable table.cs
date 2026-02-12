using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaManagement.Migrations
{
    /// <inheritdoc />
    public partial class addfkandothercolumninpayabletable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "payment_id",
                schema: "accounting",
                table: "payable",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "txn_date",
                schema: "accounting",
                table: "payable",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "ix_payable_stakeholder_id",
                schema: "accounting",
                table: "payable",
                column: "stakeholder_id");

            migrationBuilder.AddForeignKey(
                name: "fk_payable_stakeholders_stakeholder_id",
                schema: "accounting",
                table: "payable",
                column: "stakeholder_id",
                principalSchema: "stakeholder",
                principalTable: "stakeholders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payable_stakeholders_stakeholder_id",
                schema: "accounting",
                table: "payable");

            migrationBuilder.DropIndex(
                name: "ix_payable_stakeholder_id",
                schema: "accounting",
                table: "payable");

            migrationBuilder.DropColumn(
                name: "payment_id",
                schema: "accounting",
                table: "payable");

            migrationBuilder.DropColumn(
                name: "txn_date",
                schema: "accounting",
                table: "payable");
        }
    }
}
