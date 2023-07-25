CREATE TABLE [dbo].[tblEvents]
(
	[EventId] UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL, 
	[Event] NVARCHAR(1000) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[Banner] VARCHAR(100),
	[EventTypeId] INT NOT NULL,
	[ActivationDate] DATETIME NOT NULL,
	[CreatedDate] DATETIME NOT NULL,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[LastUpdatedDate] DATETIME,
	[LastUpdatedBy] UNIQUEIDENTIFIER NULL,
	[IsActive] BIT
    CONSTRAINT [PK_tblEvents] PRIMARY KEY ([EventId]), 
    CONSTRAINT [FK_tblEvents_tblEventTypes] FOREIGN KEY ([EventTypeId]) REFERENCES [tblEventTypes]([EventTypeId]), 
	CONSTRAINT [FK_tblEvents_tblUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [tblUsers]([UserId]), 
	CONSTRAINT [FK_tblEvents_tblUsers_LastUpdatedBy] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [tblUsers]([UserId]), 
)
