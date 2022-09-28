USE [GeekStore]
GO

CREATE TABLE [dbo].[ShoppingCart](
    [ShoppingCartID] int IDENTITY(1,1) PRIMARY KEY,
    [UserID] int NOT NULL,
    [BookID] int FOREIGN KEY REFERENCES dbo.Books(BookID)
    )
GO