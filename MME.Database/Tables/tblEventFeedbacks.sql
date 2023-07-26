CREATE TABLE [dbo].[tblEventFeedbacks]
(
	[Id] UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL, 
    [Liked] BIT,
    [DisLiked] BIT,
    [ReportAbuse] BIT,
    [Participation] BIT,
    [Donation] Decimal,
    [Suggestion] NVARCHAR(1000),
    [Feedback] NVARCHAR(1000),
	[EventId] UNIQUEIDENTIFIER,
    [UserId] UNIQUEIDENTIFIER,
    [Created] DATETIME,
    CONSTRAINT [PK_tblEventLikeDislikes] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_tblEventLikeDislikes_tblEvents] FOREIGN KEY ([EventId]) REFERENCES [tblEvents]([EventId]), 
    CONSTRAINT [FK_tblEventLikeDislikes_tblUsers] FOREIGN KEY ([UserId]) REFERENCES [tblUsers]([UserId]) 
)