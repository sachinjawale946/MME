
CREATE TABLE [dbo].[tblErrorLogs]
(
	[Id] INT NOT NULL IDENTITY, 
	[Message] VARCHAR(1000),
	[ErrorDetails] VARCHAR(8000),
	[Controller] VARCHAR(100),
	[Action] VARCHAR(100),
	[Query] VARCHAR(200),
	[Username] VARCHAR(50),
	[CreatedOn] DATETIME
    CONSTRAINT [PK_tblErrorLogs] PRIMARY KEY ([Id]) 
)
