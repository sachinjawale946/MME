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
INSERT INTO tblEventTypes
(EventType,DisplayOrder,IsActive)
VALUES
('General',1,1)

INSERT INTO tblEventTypes
(EventType,DisplayOrder,IsActive)
VALUES
('Participation',2,1)

INSERT INTO tblEventTypes
(EventType,DisplayOrder,IsActive)
VALUES
('Fundraiser',3,1)

INSERT INTO tblEventTypes
(EventType,DisplayOrder,IsActive)
VALUES
('Charity',4,1)

INSERT INTO tblEventTypes
(EventType,DisplayOrder,IsActive)
VALUES
('Conferences',5,1)