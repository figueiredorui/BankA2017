CREATE TABLE [dbo].[Account] (
    [AccountID]   INT           NOT NULL,
    [Description] NVARCHAR (50) NOT NULL,
    [BankName]    NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([AccountID] ASC)
);

