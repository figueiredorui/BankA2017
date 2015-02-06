CREATE TABLE [dbo].[Transaction] (
    [ID]             INT            NOT NULL,
    [AccountID]      INT            NOT NULL,
    [TransationDate] DATE           NOT NULL,
    [Description]    NVARCHAR (500) NULL,
    [DebitAmount]    DECIMAL (18)   NOT NULL,
    [CreditAmount]   DECIMAL (18)   NOT NULL,
    [Tag]            NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([ID] ASC)
);

