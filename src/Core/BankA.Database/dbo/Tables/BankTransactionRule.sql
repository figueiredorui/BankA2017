CREATE TABLE [dbo].[BankTransactionRule]
(
	[RuleID] INT NOT NULL PRIMARY KEY,
	Description NVARCHAR (50) ,
	Tag NVARCHAR (50) ,
	TagGroup NVARCHAR (50) ,
	[CreatedOn]        DATETIME      NULL,
    [CreatedBy]        NVARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
    [ChangedOn]        DATETIME      NULL,
    [ChangedBy]        NVARCHAR (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
)
