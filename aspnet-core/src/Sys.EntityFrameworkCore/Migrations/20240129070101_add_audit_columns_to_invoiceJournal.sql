
BEGIN TRANSACTION;
GO

ALTER TABLE [DOT_InvoiceJournals] ADD [CreationTime] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [DOT_InvoiceJournals] ADD [CreatorId] uniqueidentifier NULL;
GO

ALTER TABLE [DOT_InvoiceJournals] ADD [DeleterId] uniqueidentifier NULL;
GO

ALTER TABLE [DOT_InvoiceJournals] ADD [DeletionTime] datetime2 NULL;
GO

ALTER TABLE [DOT_InvoiceJournals] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [DOT_InvoiceJournals] ADD [LastModificationTime] datetime2 NULL;
GO

ALTER TABLE [DOT_InvoiceJournals] ADD [LastModifierId] uniqueidentifier NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240129070101_add_audit_columns_to_invoiceJournal', N'7.0.10');
GO

COMMIT;
GO

