
BEGIN TRANSACTION;
GO

CREATE TABLE [DOT_Files] (
    [Id] uniqueidentifier NOT NULL,
    [TenantId] uniqueidentifier NULL,
    [ParentId] uniqueidentifier NULL,
    [FileContainerName] nvarchar(450) NULL,
    [FileName] nvarchar(450) NULL,
    [MimeType] nvarchar(max) NULL,
    [FileType] int NOT NULL,
    [SubFilesQuantity] int NOT NULL,
    [ByteSize] bigint NOT NULL,
    [Hash] nvarchar(450) NULL,
    [BlobName] nvarchar(450) NULL,
    [OwnerUserId] uniqueidentifier NULL,
    [Flag] nvarchar(max) NULL,
    [ExtraProperties] nvarchar(max) NOT NULL,
    [ConcurrencyStamp] nvarchar(40) NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorId] uniqueidentifier NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierId] uniqueidentifier NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    [DeleterId] uniqueidentifier NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_DOT_Files] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [DOT_InvoiceJournals] (
    [Id] uniqueidentifier NOT NULL,
    [SupplierName] nvarchar(max) NOT NULL,
    [SupplierTIN] nvarchar(max) NOT NULL,
    [SupplierIdentificationNo] nvarchar(max) NOT NULL,
    [SupplierSSTRegistrationNo] nvarchar(max) NULL,
    [SupplierTourismTaxRegistrationNo] nvarchar(max) NULL,
    [SupplierEmail] nvarchar(max) NOT NULL,
    [SupplierMSICCode] nvarchar(max) NOT NULL,
    [SupplierBizActivityDesc] nvarchar(max) NOT NULL,
    [SupplierAddress] nvarchar(max) NOT NULL,
    [SupplierContactNo] nvarchar(max) NOT NULL,
    [BuyerName] nvarchar(max) NOT NULL,
    [BuyerTIN] nvarchar(max) NOT NULL,
    [BuyerIdentificationNo] nvarchar(max) NOT NULL,
    [BuyerSSTRegistrationNo] nvarchar(max) NULL,
    [BuyerEmail] nvarchar(max) NOT NULL,
    [BuyerAddress] nvarchar(max) NOT NULL,
    [BuyerContactNo] nvarchar(max) NOT NULL,
    [EInvoiceVersion] nvarchar(max) NOT NULL,
    [EInvoiceType] nvarchar(max) NOT NULL,
    [EInvoiceCode] nvarchar(max) NOT NULL,
    [EInvoiceOriginalReferNo] nvarchar(max) NOT NULL,
    [EInvoiceDateTime] datetime2 NOT NULL,
    [EInvoiceValidationDateTime] datetime2 NOT NULL,
    [IssuerDigitalSignature] nvarchar(max) NOT NULL,
    [CurrencyCode] nvarchar(max) NOT NULL,
    [CurrencyExchangeRate] decimal(18,2) NOT NULL,
    [FrequencyOfBilling] nvarchar(max) NOT NULL,
    [BillingPeriod] nvarchar(max) NOT NULL,
    [IRBMUniqueIdentifierNo] nvarchar(max) NOT NULL,
    [Classification] nvarchar(max) NOT NULL,
    [ProductServiceDesc] nvarchar(max) NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [TaxType] nvarchar(max) NOT NULL,
    [TaxRate] decimal(18,2) NOT NULL,
    [TaxAmount] decimal(18,2) NOT NULL,
    [TaxExemptionDetail] nvarchar(max) NULL,
    [TaxExemptedAmount] decimal(18,2) NULL,
    [SubTotal] decimal(18,2) NOT NULL,
    [TotalExcludingTax] decimal(18,2) NOT NULL,
    [TotalIncludingTax] decimal(18,2) NOT NULL,
    [Quantity] decimal(18,2) NULL,
    [Measurement] nvarchar(max) NULL,
    [DiscountRate] decimal(18,2) NULL,
    [DiscountAmount] decimal(18,2) NULL,
    [PaymentMode] nvarchar(max) NULL,
    [SupplierBankAccountNo] nvarchar(max) NULL,
    [PaymentTerms] nvarchar(max) NULL,
    [PaymentAmount] decimal(18,2) NULL,
    [PaymentDate] datetime2 NULL,
    [PaymentReferNo] nvarchar(max) NULL,
    [BillReferNo] nvarchar(max) NULL,
    [ShippingRecipientName] nvarchar(max) NOT NULL,
    [ShippingRecipientAddress] nvarchar(max) NOT NULL,
    [ShippingRecipientTIN] nvarchar(max) NOT NULL,
    [ShippingRecipientIdentification] nvarchar(max) NOT NULL,
    [CustomsForm1ReferenceNumber] nvarchar(max) NOT NULL,
    [Incoterms] nvarchar(max) NOT NULL,
    [ProductTariffCode] nvarchar(max) NULL,
    [FTAInformation] nvarchar(max) NULL,
    [AuthorisationNo] nvarchar(max) NULL,
    [CustomsForm2ReferenceNumber] nvarchar(max) NULL,
    [CountryOfOrigin] nvarchar(max) NULL,
    [OtherCharges] nvarchar(max) NULL,
    [ExtraProperties] nvarchar(max) NOT NULL,
    [ConcurrencyStamp] nvarchar(40) NOT NULL,
    CONSTRAINT [PK_DOT_InvoiceJournals] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_DOT_Files_BlobName] ON [DOT_Files] ([BlobName]);
GO

CREATE INDEX [IX_DOT_Files_FileName_OwnerUserId_FileContainerName] ON [DOT_Files] ([FileName], [OwnerUserId], [FileContainerName]);
GO

CREATE INDEX [IX_DOT_Files_Hash] ON [DOT_Files] ([Hash]);
GO

CREATE INDEX [IX_DOT_Files_ParentId_OwnerUserId_FileContainerName_FileType] ON [DOT_Files] ([ParentId], [OwnerUserId], [FileContainerName], [FileType]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231121160032_add_file_management_modules', N'7.0.10');
GO

COMMIT;
GO

