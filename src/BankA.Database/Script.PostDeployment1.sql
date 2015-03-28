/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

declare @today datetime =  GETDATE();
declare @lastTDate datetime;
declare @days int;
declare @cDate datetime;
select top 1 @lastTDate = TransactionDate from BankTransaction order by TransactionDate DESC

Set @days = DATEDIFF(day, @lastTDate,@today)

WHILE(@today > @lastTDate)
BEGIN


SET @lastTDate = DATEADD(Day, 1, @lastTDate);

INSERT INTO BankTransaction(AccountID, TransactionDate, Description, DebitAmount, CreditAmount, Tag, IsTransfer)
SELECT top 5 AccountID, @lastTDate, Description, DebitAmount, CreditAmount, Tag, IsTransfer
FROM BankTransaction order by newid()

END

select *  from BankTransaction order by  TransactionDate Desc
