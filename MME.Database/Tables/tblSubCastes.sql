CREATE TABLE [dbo].[tblSubCastes]
(
	[SubCasteId] INT NOT NULL IDENTITY, 
	[SubCaste] NVARCHAR(100),
	[DisplayOrder] INT NOT NULL,
	[IsActive] BIT,
	CONSTRAINT [PK_tblSubCastes] PRIMARY KEY ([SubCasteId]), 
)
