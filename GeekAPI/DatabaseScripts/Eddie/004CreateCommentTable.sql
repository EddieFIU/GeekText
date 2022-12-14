/*
   Tuesday, September 27, 20226:07:37 PM
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
CREATE TABLE dbo.Comment
	(
	CommentID int NOT NULL,
	UserID int NOT NULL,
	Comment varchar(2000) NOT NULL,
	CreateDate datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Comment ADD CONSTRAINT
	PK_Comment PRIMARY KEY CLUSTERED 
	(
	CommentID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Comment SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
