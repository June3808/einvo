

BEGIN TRANSACTION;
GO

CREATE TABLE [DOT_ScheduleJobs] (
    [Id] uniqueidentifier NOT NULL,
    [JobName] nvarchar(max) NOT NULL,
    [JobGroup] nvarchar(max) NOT NULL,
    [JobStatus] int NOT NULL,
    [JobRunStatus] int NOT NULL,
    [LastExecuteTime] datetime2 NOT NULL,
    [MaximumRetries] int NOT NULL,
    [IntervalMinutes] int NOT NULL,
    [IntervalSeconds] int NOT NULL,
    [RunTimes] int NOT NULL,
    [StartDateTime] datetime2 NOT NULL,
    [EndDateTime] datetime2 NULL,
    [Cron] nvarchar(max) NULL,
    [CronRemark] nvarchar(max) NULL,
    [AssemblyName] nvarchar(max) NULL,
    [ClassName] nvarchar(max) NULL,
    [TriggerType] int NOT NULL,
    [TriggerId] nvarchar(100) NOT NULL,
    [NextRunTime] datetime2 NOT NULL,
    [JsonParamters] nvarchar(max) NULL,
    [MisfireInstruction] int NULL,
    [Priority] int NOT NULL,
    [RepeatForever] bit NOT NULL,
    [SelectedDaysOfWeek] nvarchar(20) NULL,
    [StartTime] time NULL,
    [EndTime] time NULL,
    [UIBatchSeq] int NOT NULL,
    [IsAdhotJob] bit NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorId] uniqueidentifier NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierId] uniqueidentifier NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    [DeleterId] uniqueidentifier NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_DOT_ScheduleJobs] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231207114021_add-taskschedulertable', N'7.0.10');
GO

COMMIT;
GO

