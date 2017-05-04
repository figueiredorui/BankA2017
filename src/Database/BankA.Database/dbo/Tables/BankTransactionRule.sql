CREATE TABLE [dbo].[BankTransactionRule] (
    [RuleID]      INT           IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (50) NOT NULL,
    [Tag]         NVARCHAR (50) NULL,
    [TagGroup]    NVARCHAR (50) NULL,
    [IsTransfer]  BIT           NOT NULL,
    [CreatedOn]   DATETIME      NULL,
    [CreatedBy]   NVARCHAR (50) NULL,
    [ChangedOn]   DATETIME      NULL,
    [ChangedBy]   NVARCHAR (50) NULL,
    CONSTRAINT [PK_RuleID] PRIMARY KEY CLUSTERED ([RuleID] ASC)
);




