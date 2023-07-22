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

INSERT INTO tblRoles
(Role, Description,DisplayOrder, IsActive)
VALUES
('Admin','Full Access To Platform',1,1)

INSERT INTO tblRoles
(Role, Description, DisplayOrder, IsActive)
VALUES
('Contributor','Access To Events Creation/View, Members',2,1)

INSERT INTO tblRoles
(Role, Description, DisplayOrder, IsActive)
VALUES
('Member','Access To Events View, Members',3,1)