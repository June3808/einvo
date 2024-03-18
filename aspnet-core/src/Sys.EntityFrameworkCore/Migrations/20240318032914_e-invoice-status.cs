using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sys.Migrations
{
    /// <inheritdoc />
    public partial class einvoicestatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EInvoiceApiRequestJSON",
                table: "DOT_InvoiceJournals",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EInvoiceApiResponseJSON",
                table: "DOT_InvoiceJournals",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EInvoiceStatus",
                table: "DOT_InvoiceJournals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "DOT_InvoiceJournals",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderNo",
                table: "DOT_InvoiceJournals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EInvoiceApiRequestJSON",
                table: "DOT_InvoiceJournals");

            migrationBuilder.DropColumn(
                name: "EInvoiceApiResponseJSON",
                table: "DOT_InvoiceJournals");

            migrationBuilder.DropColumn(
                name: "EInvoiceStatus",
                table: "DOT_InvoiceJournals");

            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "DOT_InvoiceJournals");

            migrationBuilder.DropColumn(
                name: "OrderNo",
                table: "DOT_InvoiceJournals");
        }
    }
}
