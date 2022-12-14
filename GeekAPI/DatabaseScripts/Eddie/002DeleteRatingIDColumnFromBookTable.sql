/*
   Tuesday, September 27, 20229:27:10 AM
   User: 
   Server: .
   Database: GeekStore
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Books
	DROP CONSTRAINT FK__Books__RatingID__534D60F1
GO
ALTER TABLE dbo.Rating SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Books
	DROP COLUMN RatingID
GO
ALTER TABLE dbo.Books SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
