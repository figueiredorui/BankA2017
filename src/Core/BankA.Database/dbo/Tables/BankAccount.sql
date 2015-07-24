CREATE TABLE [dbo].[BankAccount] (
    [AccountID]        INT           IDENTITY (1, 1) NOT NULL,
    [Description]      VARCHAR (50)  NOT NULL,
    [BankName]         VARCHAR (50)  NOT NULL,
    [IsSavingsAccount] BIT           NOT NULL,
    [CreatedOn]        DATETIME      NULL,
    [CreatedBy]        NVARCHAR (50) NULL,
    [ChangedOn]        DATETIME      NULL,
    [ChangedBy]        NVARCHAR (50) NULL,
    CONSTRAINT [PK_AccountID] PRIMARY KEY CLUSTERED ([AccountID] ASC)
);









