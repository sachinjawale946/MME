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
INSERT INTO tblOccupations
(Occupation, DisplayOrder,IsActive)
VALUES
('Accountant',1,1)

INSERT INTO tblOccupations
(Occupation, DisplayOrder,IsActive)
VALUES
('Business - Self Employed ',2,1)

INSERT INTO tblOccupations
(Occupation, DisplayOrder,IsActive)
VALUES
('Chartered Accountant',3,1)

INSERT INTO tblOccupations
(Occupation, DisplayOrder,IsActive)
VALUES
('Doctor',4,1)

INSERT INTO tblOccupations
(Occupation, DisplayOrder,IsActive)
VALUES
('Lawyer',5,1)

INSERT INTO tblOccupations
(Occupation, DisplayOrder,IsActive)
VALUES
('Software Engineer',6,1)