CREATE TABLE [dbo].[tblRoles]
(
	[RoleId] INT NOT NULL IDENTITY, 
	[Role] VARCHAR(50),
	[Description] VARCHAR(100),
	[DisplayOrder] INT NOT NULL,
	[IsActive] BIT,
    CONSTRAINT [PK_tblRoles] PRIMARY KEY ([RoleId]) 
)
