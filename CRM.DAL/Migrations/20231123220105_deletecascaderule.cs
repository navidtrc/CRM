using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.DAL.Migrations
{
    /// <inheritdoc />
    public partial class deletecascaderule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Person_Id",
                schema: "General",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Person_Id",
                schema: "General",
                table: "Staff");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Person_Id",
                schema: "General",
                table: "Customer",
                column: "Id",
                principalSchema: "Security",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Person_Id",
                schema: "General",
                table: "Staff",
                column: "Id",
                principalSchema: "Security",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Person_Id",
                schema: "General",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Person_Id",
                schema: "General",
                table: "Staff");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Person_Id",
                schema: "General",
                table: "Customer",
                column: "Id",
                principalSchema: "Security",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Person_Id",
                schema: "General",
                table: "Staff",
                column: "Id",
                principalSchema: "Security",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
