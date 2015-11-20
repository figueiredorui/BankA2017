CREATE TABLE [dbo].[BankFile] (
    [FileID]      INT             IDENTITY (1, 1) NOT NULL,
    [FileName]    NVARCHAR (50)   NOT NULL,
    [FileContent] VARBINARY (MAX) NOT NULL,
    [ContentType] NVARCHAR (50)   NOT NULL,
    [AccountID]   INT             NOT NULL,
    [CreatedOn]   DATETIME        NULL,
    [CreatedBy]   NVARCHAR (50)   NULL,
    [ChangedOn]   DATETIME        NULL,
    [ChangedBy]   NVARCHAR (50)   NULL,
    CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED ([FileID] ASC),
    CONSTRAINT [FK_BankFile_BankAccount] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[BankAccount] ([AccountID])
);

