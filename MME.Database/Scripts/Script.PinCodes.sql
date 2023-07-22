/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO tblPinCodes
(PinCode,State,DisplayOrder,IsActive)
VALUES
('400071',1,1,1)

INSERT INTO tblPinCodes
(PinCode,State,DisplayOrder,IsActive)
VALUES
('400024',1,2,1)

INSERT INTO tblPinCodes
(PinCode,State,DisplayOrder,IsActive)
VALUES
('400080',2,3,1)

INSERT INTO tblPinCodes
(PinCode,State,DisplayOrder,IsActive)
VALUES
('400604',2,4,1)

INSERT INTO tblPinCodes
(PinCode,State,DisplayOrder,IsActive)
VALUES
('400608',2,5,1)

INSERT INTO tblPinCodes
(PinCode,State,DisplayOrder,IsActive)
VALUES
('400612',2,6,1)
