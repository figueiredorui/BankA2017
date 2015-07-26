CREATE TABLE [dbo].[BankStatementFile] (
    [FileID]      INT             IDENTITY (1, 1) NOT NULL,
    [FileName]    NVARCHAR (50)   NOT NULL,
    [FileContent] VARBINARY (MAX) NOT NULL,
    [ContentType] NVARCHAR (50)   NOT NULL,
    [AccountID]   INT             NOT NULL,
    [CreatedOn]   DATETIME        NULL,
    [CreatedBy]   NVARCHAR (50)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [ChangedOn]   DATETIME        NULL,
    [ChangedBy]   NVARCHAR (50)   COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK_StatementFile] PRIMARY KEY CLUSTERED ([FileID] ASC),
    CONSTRAINT [FK_BankStatementFile_BankAccount] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[BankAccount] ([AccountID])
);





