using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.DAL.Migrations
{
    /// <inheritdoc />
    public partial class delete_inquiry_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inquiry",
                schema: "Basic");

            migrationBuilder.AddColumn<bool>(
                name: "InquiryConfirmation",
                schema: "Basic",
                table: "Ticket",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InquiryNeed",
                schema: "Basic",
                table: "Ticket",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InquiryConfirmation",
                schema: "Basic",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "InquiryNeed",
                schema: "Basic",
                table: "Ticket");

            migrationBuilder.CreateTable(
                name: "Inquiry",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inquiry_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "Basic",
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_TicketId",
                schema: "Basic",
                table: "Inquiry",
                column: "TicketId",
                unique: true);
        }
    }
}
