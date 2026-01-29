using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeaManagement.Migrations
{
    /// <inheritdoc />
    public partial class Salesnoandbillnoinsalestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bill_no",
                schema: "inventory",
                table: "sales",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sale_no",
                schema: "inventory",
                table: "sales",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bill_no",
                schema: "inventory",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "sale_no",
                schema: "inventory",
                table: "sales");
        }
    }
}
