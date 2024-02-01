
BEGIN TRANSACTION;
GO

ALTER TABLE [DOT_InvoiceJournals] ADD [ProcessingStatus] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240131062704_add_ProcessingStatus_to_invoiceJournal', N'7.0.10');
GO

COMMIT;
GO

