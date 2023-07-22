CREATE TABLE [dbo].[tblSubCastes]
(
	[SubCasteId] INT NOT NULL IDENTITY, 
	[SubCaste] NVARCHAR(100),
	[Caste] INT NOT NULL,
	[DisplayOrder] INT NOT NULL,
	[IsActive] BIT,
	CONSTRAINT [PK_tblSubCastes] PRIMARY KEY ([SubCasteId]), 
    CONSTRAINT [FK_tblSubCastes_tblCastes] FOREIGN KEY ([Caste]) REFERENCES [tblCastes]([CasteId]),
)
