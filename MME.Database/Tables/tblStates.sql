CREATE TABLE [dbo].[tblStates]
(
	[StateId] INT NOT NULL  IDENTITY, 
	[State] VARCHAR(100),
	[DisplayOrder] INT NOT NULL,
	[IsActive] BIT,
    CONSTRAINT [PK_tblStates] PRIMARY KEY ([StateId])
)
