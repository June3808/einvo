using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sys.Migrations
{
    /// <inheritdoc />
    public partial class addfilemanagementmodules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DOT_Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FileContainerName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    SubFilesQuantity = table.Column<int>(type: "int", nullable: false),
                    ByteSize = table.Column<long>(type: "bigint", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BlobName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OwnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOT_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DOT_InvoiceJournals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierTIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierIdentificationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierSSTRegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierTourismTaxRegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierMSICCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierBizActivityDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerTIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerIdentificationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerSSTRegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EInvoiceVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EInvoiceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EInvoiceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EInvoiceOriginalReferNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EInvoiceDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EInvoiceValidationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuerDigitalSignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FrequencyOfBilling = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingPeriod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IRBMUniqueIdentifierNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductServiceDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxExemptionDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxExemptedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalExcludingTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalIncludingTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Measurement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierBankAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTerms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentReferNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillReferNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingRecipientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingRecipientAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingRecipientTIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingRecipientIdentification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomsForm1ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Incoterms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductTariffCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FTAInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorisationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomsForm2ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherCharges = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOT_InvoiceJournals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DOT_Files_BlobName",
                table: "DOT_Files",
                column: "BlobName");

            migrationBuilder.CreateIndex(
                name: "IX_DOT_Files_FileName_OwnerUserId_FileContainerName",
                table: "DOT_Files",
                columns: new[] { "FileName", "OwnerUserId", "FileContainerName" });

            migrationBuilder.CreateIndex(
                name: "IX_DOT_Files_Hash",
                table: "DOT_Files",
                column: "Hash");

            migrationBuilder.CreateIndex(
                name: "IX_DOT_Files_ParentId_OwnerUserId_FileContainerName_FileType",
                table: "DOT_Files",
                columns: new[] { "ParentId", "OwnerUserId", "FileContainerName", "FileType" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DOT_Files");

            migrationBuilder.DropTable(
                name: "DOT_InvoiceJournals");
        }
    }
}
