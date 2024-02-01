using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sys.Migrations
{
    /// <inheritdoc />
    public partial class addauditcolumnstoinvoiceJournal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "DOT_InvoiceJournals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "DOT_InvoiceJournals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "DOT_InvoiceJournals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "DOT_InvoiceJournals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DOT_InvoiceJournals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "DOT_InvoiceJournals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "DOT_InvoiceJournals",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "DOT_InvoiceJournals");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "DOT_InvoiceJournals");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "DOT_InvoiceJournals");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "DOT_InvoiceJournals");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DOT_InvoiceJournals");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "DOT_InvoiceJournals");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "DOT_InvoiceJournals");
        }
    }
}
