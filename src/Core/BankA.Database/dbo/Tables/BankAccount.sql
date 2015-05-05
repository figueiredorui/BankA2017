CREATE TABLE [dbo].[BankAccount] (
    [AccountID]        INT           IDENTITY (1, 1) NOT NULL,
    [Description]      VARCHAR (50)  NOT NULL,
    [BankName]         VARCHAR (50)  NOT NULL,
    [IsSavingsAccount] BIT           NOT NULL,
    [CreatedOn]        DATETIME      NULL,
    [CreatedBy]        NVARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [ChangedOn]        DATETIME      NULL,
    [ChangedBy]        NVARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    CONSTRAINT [PK__BankAcco__349DA586FF5445F7] PRIMARY KEY CLUSTERED ([AccountID] ASC)
);







