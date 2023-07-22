CREATE TABLE [dbo].[tblCastes]
(
	[CasteId] INT NOT NULL IDENTITY, 
	[Caste] NVARCHAR(100),
	[DisplayOrder] INT NOT NULL,
	[IsActive] BIT,
    CONSTRAINT [PK_tblCastes] PRIMARY KEY ([CasteId]) 
)
