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

DECLARE @version NVARCHAR(50) = '1.0.0'

SELECT * FROM BankVersion
IF (@@ROWCOUNT = 0)
BEGIN
	INSERT BankVersion Values (@version)
END
ELSE
BEGIN
	UPDATE BankVersion SET Version = @version
END