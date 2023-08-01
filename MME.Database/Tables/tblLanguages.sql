﻿CREATE TABLE [dbo].[tblLanguages]
(
	[LanguageId] INT NOT NULL IDENTITY, 
	[Language] NVARCHAR(100),
	[LanguageCode] NVARCHAR(10),
	[DisplayOrder] INT NOT NULL,
	[IsActive] BIT,
    CONSTRAINT [PK_tblLanguages] PRIMARY KEY ([LanguageId]) 
)
