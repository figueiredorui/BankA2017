CREATE TABLE [BankAccount] (
	[AccountID]	integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	[Description]	varchar(50) NOT NULL COLLATE NOCASE,
	[BankName]	varchar(50) NOT NULL COLLATE NOCASE,
	[IsSavingsAccount]	bit NOT NULL,
	[CreatedOn]	datetime,
	[CreatedBy]	nvarchar(50) COLLATE NOCASE,
	[ChangedOn]	datetime,
	[ChangedBy]	nvarchar(50) COLLATE NOCASE,
	[RowVersion]	blob NOT NULL

);
CREATE TABLE sqlite_sequence(name,seq);
CREATE TABLE [BankStatementFile] (
	[FileID]	integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	[FileName]	nvarchar(50) NOT NULL COLLATE NOCASE,
	[FileContent]	blob NOT NULL,
	[ContentType]	nvarchar(50) NOT NULL COLLATE NOCASE,
	[CreatedOn]	datetime,
	[CreatedBy]	nvarchar(50) COLLATE NOCASE,
	[ChangedOn]	datetime,
	[ChangedBy]	nvarchar(50) COLLATE NOCASE,
	[RowVersion]	blob NOT NULL

);
CREATE TABLE [BankTransaction] (
	[ID]	integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	[AccountID]	integer NOT NULL,
	[TransactionDate]	datetime NOT NULL COLLATE NOCASE,
	[Description]	varchar(500) COLLATE NOCASE,
	[DebitAmount]	numeric NOT NULL,
	[CreditAmount]	numeric NOT NULL,
	[Tag]	varchar(50) COLLATE NOCASE,
	[IsTransfer]	bit NOT NULL,
	[FileID]	integer,
	[CreatedOn]	datetime,
	[CreatedBy]	nvarchar(50) COLLATE NOCASE,
	[ChangedOn]	datetime,
	[ChangedBy]	nvarchar(50) COLLATE NOCASE,
	[RowVersion]	blob NOT NULL
,
    FOREIGN KEY ([AccountID])
        REFERENCES [BankAccount]([AccountID])
);
CREATE TABLE [BankVersion] (
	[Version]	nvarchar(50) NOT NULL COLLATE NOCASE,
    PRIMARY KEY ([Version])

);
;
