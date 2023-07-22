CREATE TABLE [dbo].[tblPinCodes]
(
	[PinCodeId] INT NOT NULL  IDENTITY, 
	[PinCode] VARCHAR(100),
	[State] INT NOT NULL,
	[DisplayOrder] INT NOT NULL,
	[IsActive] BIT,
    CONSTRAINT [PK_tblPinCodes] PRIMARY KEY ([PinCodeId]), 
    CONSTRAINT [FK_tblPinCodes_tblStates] FOREIGN KEY ([State]) REFERENCES [tblStates]([StateId])
)
