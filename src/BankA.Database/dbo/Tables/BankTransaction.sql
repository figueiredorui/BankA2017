﻿CREATE TABLE [dbo].[BankTransaction] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [AccountID]      INT           NOT NULL,
    [TransationDate] DATE          NOT NULL,
    [Description]    VARCHAR (500) NULL,
    [DebitAmount]    DECIMAL (18)  NOT NULL,
    [CreditAmount]   DECIMAL (18)  NOT NULL,
    [Tag]            VARCHAR (50)  NULL,
    [CreatedOn]      DATETIME      NULL,
    [CreatedBy]      NVARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [ChangedOn]      DATETIME      NULL,
    [ChangedBy]      NVARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [RowVersion]     ROWVERSION    NOT NULL,
    CONSTRAINT [PK__BankTran__3214EC278FE2CE39] PRIMARY KEY CLUSTERED ([ID] ASC)
);



