USE [GeekStore]
GO

CREATE TABLE [dbo].[USERS](
    [UserID] int IDENTITY(1,1) PRIMARY KEY,
    [Username] VARCHAR(20) NOT NULL,
    [Password] VARCHAR(20) NOT NULL,
    [Name] VARCHAR(max) NOT NULL,
    [Email] VARCHAR(20) NOT NULL,
    [HomeAddress] VARCHAR(20) NOT NULL
  
    )
GO