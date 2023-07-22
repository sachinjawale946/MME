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

-- ROLES
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
-- ROLES

-- STATES
INSERT INTO tblStates
(State,DisplayOrder,IsActive)
VALUES
('Maharashtra',1,1)

INSERT INTO tblStates
(State,DisplayOrder,IsActive)
VALUES
('Gujrat',2,1)
-- STATES

-- PINCODES
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
-- PINCODES

-- CASTES
INSERT INTO tblCastes
(Caste,DisplayOrder,IsActive)
VALUES 
('General',1,1)

INSERT INTO tblCastes
(Caste,DisplayOrder,IsActive)
VALUES 
('OBC',2,1)

INSERT INTO tblCastes
(Caste,DisplayOrder,IsActive)
VALUES 
('SC',3,1)

INSERT INTO tblCastes
(Caste,DisplayOrder,IsActive)
VALUES 
('ST',4,1)
-- CASTES

-- EVENT TYPES
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
-- EVENT TYPES

-- LANGUAGES
INSERT INTO tblLanguages
(Language,DisplayOrder, IsActive)
VALUES
('English',1,1)

INSERT INTO tblLanguages
(Language,DisplayOrder,IsActive)
VALUES
('Hindi',2,1)

INSERT INTO tblLanguages
(Language,DisplayOrder,IsActive)
VALUES
('Marathi',3,1)

INSERT INTO tblLanguages
(Language,DisplayOrder,IsActive)
VALUES
('Gujrati',4,1)
-- LANGUAGES

-- OCCUPATIONS
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
-- OCCUPATIONS

-- RELIGIOUS
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
-- RELIGIOUS