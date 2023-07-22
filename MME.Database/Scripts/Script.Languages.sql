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
INSERT INTO tblLanguages
(Language,DisplayOrder, IsActive)
VALUES
('English',1,1)

INSERT INTO tblLanguages
(Language,IsActive)
VALUES
('Hindi',2,1)

INSERT INTO tblLanguages
(Language,IsActive)
VALUES
('Marathi',3,1)

INSERT INTO tblLanguages
(Language,IsActive)
VALUES
('Gujrati',4,1)