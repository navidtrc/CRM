using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addTempCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireTempCodeTime",
                schema: "Security",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TempCode",
                schema: "Security",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireTempCodeTime",
                schema: "Security",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TempCode",
                schema: "Security",
                table: "User");
        }
    }
}
