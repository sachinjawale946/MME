CREATE TABLE [dbo].[tblReligions]
(
	[ReligionId] INT NOT NULL  IDENTITY, 
	[Religion] VARCHAR(100),
	[DisplayOrder] INT NOT NULL,
	[IsActive] BIT,
    CONSTRAINT [PK_tblReligions] PRIMARY KEY ([ReligionId])
)
