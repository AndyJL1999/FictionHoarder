﻿CREATE TABLE [dbo].[Story]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Author] NVARCHAR(50) NOT NULL, 
    [Summary] NVARCHAR(1000) NOT NULL, 
    [Chapters] VARCHAR(3) NOT NULL,
    [EpubFile] NVARCHAR(MAX) NOT NULL default('No File Path')
)
