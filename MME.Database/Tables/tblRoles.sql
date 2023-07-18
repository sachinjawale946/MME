CREATE TABLE [dbo].[tblRoles]
(
	[RoleId] INT NOT NULL IDENTITY, 
	[Role] VARCHAR(50),
	[Description] VARCHAR(100),
	[IsActive] BIT,
    CONSTRAINT [PK_tblRoles] PRIMARY KEY ([RoleId]) 
)
