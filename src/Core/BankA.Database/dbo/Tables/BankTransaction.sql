CREATE TABLE [dbo].[BankTransaction] (
    [ID]              INT             IDENTITY (1, 1) NOT NULL,
    [AccountID]       INT             NOT NULL,
    [TransactionDate] DATE            NOT NULL,
    [Description]     VARCHAR (500)   NULL,
    [DebitAmount]     DECIMAL (18, 2) NOT NULL,
    [CreditAmount]    DECIMAL (18, 2) NOT NULL,
    [Tag]             VARCHAR (50)    NULL,
    [TagGroup]        NVARCHAR (50)   NULL,
    [IsTransfer]      BIT             NOT NULL,
    [FileID]          INT             NULL,
    [CreatedOn]       DATETIME        NULL,
    [CreatedBy]       NVARCHAR (50)   NULL,
    [ChangedOn]       DATETIME        NULL,
    [ChangedBy]       NVARCHAR (50)   NULL,
    CONSTRAINT [PK_ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BankTransaction_BankAccount] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[BankAccount] ([AccountID])
);













