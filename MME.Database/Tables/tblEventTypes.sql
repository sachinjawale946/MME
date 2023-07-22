CREATE TABLE [dbo].[tblEventTypes]
(
	[EventTypeId] INT NOT NULL  IDENTITY, 
	[EventType] VARCHAR(200) NOT NULL,
	[DisplayOrder] INT,
	[IsActive] BIT,
    CONSTRAINT [PK_tblEventTypes] PRIMARY KEY ([EventTypeId])
)
