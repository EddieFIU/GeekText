USE [GeekStore]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Books](
	[BookID] int NOT NULL PRIMARY KEY,
	[Title] VARCHAR(2000) NOT NULL,
	[Description] VARCHAR(max) NOT NULL,
	[Price] smallmoney NOT NULL,
	[Genre] VARCHAR(2000) NOT NULL,
	[Publisher] VARCHAR(2000) NOT NULL,
	[YearPublished] smallint NOT NULL,
	[CopiesSold] int NOT NULL,
	[RatingID] int,
	[AuthorID] int,
	)
GO