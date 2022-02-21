using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Infrastructure.Migrations
{
    public partial class somechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Price",
                schema: "Ticket",
                table: "Inquiry",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "InquiryId",
                schema: "Ticket",
                table: "Device",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InquiryId",
                schema: "Ticket",
                table: "Device");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "Ticket",
                table: "Inquiry",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
