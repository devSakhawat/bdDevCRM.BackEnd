-- Create AuditLogs table for comprehensive audit trail
CREATE TABLE [dbo].[AuditLogs] (
    [AuditId] BIGINT IDENTITY(1,1) PRIMARY KEY,

    -- Who
    [UserId] INT NULL,
    [Username] NVARCHAR(100) NULL,
    [IpAddress] NVARCHAR(50) NULL,
    [UserAgent] NVARCHAR(500) NULL,

    -- What
    [Action] NVARCHAR(50) NOT NULL,
    [EntityType] NVARCHAR(100) NOT NULL,
    [EntityId] NVARCHAR(100) NULL,
    [Endpoint] NVARCHAR(200) NULL,
    [Module] NVARCHAR(100) NULL,

    -- Details
    [OldValue] NVARCHAR(MAX) NULL,
    [NewValue] NVARCHAR(MAX) NULL,
    [Changes] NVARCHAR(MAX) NULL,

    -- When
    [Timestamp] DATETIME2(7) NOT NULL DEFAULT GETUTCDATE(),

    -- Context
    [CorrelationId] NVARCHAR(100) NULL,
    [SessionId] NVARCHAR(100) NULL,
    [RequestId] NVARCHAR(100) NULL,

    -- Result
    [Success] BIT NOT NULL DEFAULT 1,
    [StatusCode] INT NULL,
    [ErrorMessage] NVARCHAR(2000) NULL,
    [DurationMs] INT NULL,

    CONSTRAINT [FK_AuditLogs_Users] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[Users]([UserId]) ON DELETE SET NULL
);

-- Indexes for performance
CREATE INDEX [IX_AuditLogs_UserId] ON [dbo].[AuditLogs]([UserId]);
CREATE INDEX [IX_AuditLogs_Timestamp] ON [dbo].[AuditLogs]([Timestamp] DESC);
CREATE INDEX [IX_AuditLogs_EntityType_EntityId] ON [dbo].[AuditLogs]([EntityType], [EntityId]);
CREATE INDEX [IX_AuditLogs_Action] ON [dbo].[AuditLogs]([Action]);
CREATE INDEX [IX_AuditLogs_CorrelationId] ON [dbo].[AuditLogs]([CorrelationId]);
CREATE INDEX [IX_AuditLogs_UserAction] ON [dbo].[AuditLogs]([UserId], [Action], [Timestamp] DESC);

GO
