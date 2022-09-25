﻿USE [GeekStore]
GO

CREATE TABLE [dbo].[Authors](
    [AuthorID] int NOT NULL PRIMARY KEY,
    [FirstName] VARCHAR(2000) NOT NULL,
    [LastName] VARCHAR(2000) NOT NULL,
    [Biography] VARCHAR(max) NOT NULL,
    [Publisher] VARCHAR(2000) NOT NULL
    )
GO