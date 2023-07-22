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
INSERT INTO tblReligions
(Religion,DisplayOrder,IsActive)
VALUES
('Hindu',1,1)

INSERT INTO tblReligions
(Religion,DisplayOrder,IsActive)
VALUES
('Muslim',2,1)

INSERT INTO tblReligions
(Religion,DisplayOrder,IsActive)
VALUES
('Christian',3,1)

INSERT INTO tblReligions
(Religion,DisplayOrder,IsActive)
VALUES
('Sikh',4,1)


INSERT INTO tblReligions
(Religion,DisplayOrder,IsActive)
VALUES
('Buddhisht',5,1)

INSERT INTO tblReligions
(Religion,DisplayOrder,IsActive)
VALUES
('Other',6,1)