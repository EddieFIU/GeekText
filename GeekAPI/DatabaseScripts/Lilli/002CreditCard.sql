USE [GeekStore]
GO

CREATE TABLE [dbo].[CREDITCARDS](
    [CCID] int IDENTITY(1,1) PRIMARY KEY,
    [CardNumber] VARCHAR(16) NOT NULL,
    [ExpDate] DATETIME NOT NULL,
    [PIN] INT NOT NULL,
    [Name] VARCHAR (20) NOT NULL,
    [ZipCode] INT NOT NULL
  
    )
GO