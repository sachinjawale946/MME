CREATE TABLE [dbo].[tblOccupations]
(
	[OccupationId] INT NOT NULL IDENTITY, 
	[Occupation] VARCHAR(100),
	[DisplayOrder] INT NOT NULL,
	[IsActive] BIT,
    CONSTRAINT [PK_tblOccupations] PRIMARY KEY ([OccupationId]) 
)
