﻿CREATE TABLE [dbo].[tblUsers]
(
	[UserId] UNIQUEIDENTIFIER DEFAULT NEWID(), 
	[Username] NVARCHAR(50) NOT NULL,
	[InviteCode] NVARCHAR(50),
	[Password] VARCHAR(500) NOT NULL,
	[PasswordSalt] VARCHAR(500) NOT NULL,
	[FirstName] VARCHAR(50) NOT NULL,
	[MiddleName] VARCHAR(50),
	[LastName] VARCHAR(50) NOT NULL,
	[Mobile] VARCHAR(15) NOT NULL,
	[Email] VARCHAR(50),
	[Gender] VARCHAR(10),
	[MaritalStatus] VARCHAR(10),
	[ProfilePic] VARCHAR(100),
	[Society] VARCHAR(80),
	[Area] VARCHAR(80),
	[Location] VARCHAR(80),
	[Landmark] VARCHAR(80),
	[City] VARCHAR(80),
	[State] INT,
	[Pincode] INT,
	[Latitude] VARCHAR(100),
	[Longtitude] VARCHAR(100),
	[MotherTounge] INT,
	[Religion] INT,
	[Caste] INT,
	[SubCaste] INT,
 	[Occupation] INT,
	[BirthDate] DATETIME,
	[RoleId] INT NOT NULL,
	[IsActive] BIT NOT NULL,
    CONSTRAINT [PK_tblUsers] PRIMARY KEY ([UserId]), 
    CONSTRAINT [FK_tblUsers_tblRoles] FOREIGN KEY ([RoleId]) REFERENCES [tblRoles]([RoleId]), 
    CONSTRAINT [FK_tblUsers_tblLanguages] FOREIGN KEY ([MotherTounge]) REFERENCES [tblLanguages]([LanguageId]), 
    CONSTRAINT [FK_tblUsers_tblStates] FOREIGN KEY ([State]) REFERENCES [tblStates]([StateId]),
	CONSTRAINT [FK_tblUsers_tblPinCodes] FOREIGN KEY (PinCode) REFERENCES [tblPinCodes]([PinCodeId]), 
    CONSTRAINT [FK_tblUsers_tblOccupations] FOREIGN KEY ([Occupation]) REFERENCES [tblOccupations]([OccupationId]), 
    CONSTRAINT [FK_tblUsers_tblReligions] FOREIGN KEY ([Religion]) REFERENCES [tblReligions]([ReligionId]),
	CONSTRAINT [FK_tblUsers_tblCastes] FOREIGN KEY ([Caste]) REFERENCES [tblCastes]([CasteId]), 
	CONSTRAINT [FK_tblUsers_tblSubCastes] FOREIGN KEY ([SubCaste]) REFERENCES [tblSubCastes]([SubCasteId]), 
)
