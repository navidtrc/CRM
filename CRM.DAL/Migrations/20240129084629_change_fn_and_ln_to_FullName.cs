using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.DAL.Migrations
{
    /// <inheritdoc />
    public partial class change_fn_and_ln_to_FullName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                schema: "Security",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "Security",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "Security",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "Security",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "Security",
                table: "Person");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Security",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Security",
                table: "Person");

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                schema: "Security",
                table: "Person",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "Security",
                table: "Person",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "Security",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                schema: "Security",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "Security",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
