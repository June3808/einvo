BEGIN TRANSACTION;
GO

ALTER TABLE [DOT_InvoiceJournals] ADD [EInvoiceApiRequestJSON] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [DOT_InvoiceJournals] ADD [EInvoiceApiResponseJSON] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [DOT_InvoiceJournals] ADD [EInvoiceStatus] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [DOT_InvoiceJournals] ADD [ErrorMessage] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [DOT_InvoiceJournals] ADD [OrderNo] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240318032914_e-invoice-status', N'7.0.10');
GO

COMMIT;
GO
