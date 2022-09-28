﻿USE [GeekStore]
GO

CREATE TABLE [dbo].[ShoppingCart](
    [ShoppingCartID] int IDENTITY(1,1) PRIMARY KEY,
    [UserID] int NOT NULL,
    [BookID] int NOT NULL FOREIGN KEY REFERENCES dbo.Books(BookID)
    )
GO