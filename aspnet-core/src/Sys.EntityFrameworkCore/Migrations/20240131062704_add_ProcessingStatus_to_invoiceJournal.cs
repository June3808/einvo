using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sys.Migrations
{
    /// <inheritdoc />
    public partial class addProcessingStatustoinvoiceJournal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessingStatus",
                table: "DOT_InvoiceJournals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessingStatus",
                table: "DOT_InvoiceJournals");
        }
    }
}
