USE [GeekStore]
GO

CREATE TABLE [dbo].[Books](
    [BookID] int IDENTITY(1,1) PRIMARY KEY,
    [ISBN] bigint NOT NULL,
    [Title] VARCHAR(2000) NOT NULL,
    [Description] VARCHAR(max) NOT NULL,
    [Price] smallmoney NOT NULL,
    [Genre] VARCHAR(2000) NOT NULL,
    [Publisher] VARCHAR(2000) NOT NULL,
    [YearPublished] smallint NOT NULL,
    [CopiesSold] int NOT NULL,
    [RatingID] int FOREIGN KEY REFERENCES dbo.Rating(RatingID),
    [AuthorID] int FOREIGN KEY REFERENCES dbo.Authors(AuthorID)
    )
GO