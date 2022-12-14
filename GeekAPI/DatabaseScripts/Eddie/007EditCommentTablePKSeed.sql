/*
   Wednesday, October 5, 20222:42:07 PM
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
ALTER TABLE dbo.Comment
	DROP CONSTRAINT FK_Comment_Books
GO
ALTER TABLE dbo.Books SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Comment
	DROP CONSTRAINT FK_Comment_Rating
GO
ALTER TABLE dbo.Rating SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Comment
	DROP CONSTRAINT FK_Comment_USERS
GO
ALTER TABLE dbo.USERS SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Comment
	(
	CommentID int NOT NULL IDENTITY (1, 1),
	UserID int NOT NULL,
	RatingID int NOT NULL,
	BookID int NOT NULL,
	Comment varchar(2000) NOT NULL,
	CreateDate datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Comment SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Comment ON
GO
IF EXISTS(SELECT * FROM dbo.Comment)
	 EXEC('INSERT INTO dbo.Tmp_Comment (CommentID, UserID, RatingID, BookID, Comment, CreateDate)
		SELECT CommentID, UserID, RatingID, BookID, Comment, CreateDate FROM dbo.Comment WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Comment OFF
GO
DROP TABLE dbo.Comment
GO
EXECUTE sp_rename N'dbo.Tmp_Comment', N'Comment', 'OBJECT' 
GO
ALTER TABLE dbo.Comment ADD CONSTRAINT
	PK_Comment PRIMARY KEY CLUSTERED 
	(
	CommentID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE UNIQUE NONCLUSTERED INDEX IX_Rating ON dbo.Comment
	(
	RatingID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Comment ADD CONSTRAINT
	FK_Comment_USERS FOREIGN KEY
	(
	UserID
	) REFERENCES dbo.USERS
	(
	UserID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Comment ADD CONSTRAINT
	FK_Comment_Rating FOREIGN KEY
	(
	RatingID
	) REFERENCES dbo.Rating
	(
	RatingID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Comment ADD CONSTRAINT
	FK_Comment_Books FOREIGN KEY
	(
	BookID
	) REFERENCES dbo.Books
	(
	BookID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
