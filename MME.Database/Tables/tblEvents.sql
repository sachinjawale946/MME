CREATE TABLE [dbo].[tblEvents]
(
	[EventId] UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL, 
	[Event] NVARCHAR(1000) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[Banner] VARCHAR(100),
	[EventType] INT NOT NULL,
	[ActivationDate] DATETIME NOT NULL,
	[CreatedDate] DATETIME NOT NULL,
	[CreatedBy] INT,
	[IsActive] BIT
    CONSTRAINT [PK_tblEvents] PRIMARY KEY ([EventId]), 
    CONSTRAINT [FK_tblEvents_tblEventTypes] FOREIGN KEY ([EventType]) REFERENCES [tblEventTypes]([EventTypeId]), 
)
