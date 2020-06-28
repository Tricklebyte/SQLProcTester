USE [SqlProcTest]
GO

/****** Object: Table [dbo].[EmployeePositionHistory] Script Date: 6/28/2020 6:05:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmployeePositionHistory] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [LastName]      NVARCHAR (50) NULL,
    [FirstName]     NCHAR (50)    NULL,
    [MiddleInitial] CHAR (1)      NULL,
    [DateOfBirth]   DATETIME      NULL,
    [IsContractor]  BIT           NULL,
    [Position]      VARCHAR (50)  NULL,
    [StartDate]     DATETIME2 (7) NULL,
    [EndDate]       DATETIME2 (7) NULL
);


GO

--=======================================
-- Populate Test Records

SET IDENTITY_INSERT [dbo].[EmployeePositionHistory] ON
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (1, N'Barker', N'Bob                                               ', N'B', N'1968-01-02 00:00:00', 1, N'Master of Bananas', N'2018-01-01 00:00:00', N'2018-07-24 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (2, N'Barker', N'Bob                                               ', N'B', N'1968-01-02 00:00:00', 1, N'Space Travel Agent', N'2018-07-25 00:00:00', N'2019-03-05 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (3, N'Barker', N'Bob                                               ', N'B', N'1968-01-02 00:00:00', 1, N'Bodhisattva', N'2020-02-03 00:00:00', NULL)
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (4, N'Baker', N'Bill                                              ', N'B', N'1972-02-03 00:00:00', 0, N'Cheif Assitant to the Assistant Cheif', N'2010-05-01 00:00:00', N'2012-08-26 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (5, N'Baker', N'Bill                                              ', N'B', N'1972-02-03 00:00:00', 0, N'Telephone Psychic', N'2016-08-27 00:00:00', N'2019-07-07 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (6, N'Baker', N'Bill                                              ', N'B', N'1972-02-03 00:00:00', 0, N'Intergalactic Surfer', N'2019-07-08 00:00:00', NULL)
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (7, N'Barkley', N'Bart                                              ', N'B', N'1980-11-12 00:00:00', 0, N'Origami Ninja', N'2010-01-02 00:00:00', N'2015-02-02 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (8, N'Barkley', N'Bart                                              ', N'B', N'1980-11-12 00:00:00', 0, N'Elected Buffoon', N'2015-02-03 00:00:00', N'2018-02-03 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (9, N'Barkley', N'Bart                                              ', N'B', N'1980-11-12 00:00:00', 0, N'Colonel of Mustard', N'2019-07-04 00:00:00', N'2020-03-02 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (10, N'Barkley', N'Bart                                              ', N'B', N'1980-11-12 00:00:00', 0, N'Human Cannonball', N'2020-05-01 00:00:00', NULL)
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (11, N'Bacon', N'Brady                                             ', N'B', N'1990-09-12 00:00:00', 0, N'Lovable Oaf', N'2001-02-03 00:00:00', N'2005-02-25 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (12, N'Bacon', N'Brady                                             ', N'B', N'1990-09-12 00:00:00', 0, N'Telephone Psychic', N'2005-02-26 00:00:00', N'2013-06-08 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (13, N'Bacon', N'Brady                                             ', N'B', N'1990-09-12 00:00:00', 0, N'Human Cannonball', N'2013-06-09 00:00:00', N'2019-12-12 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (14, N'Bacon', N'Brady                                             ', N'B', N'1990-09-12 00:00:00', 0, N'Space Travel Agent', N'2019-12-13 00:00:00', N'2020-01-01 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (15, N'Baggins', N'Bilbo                                             ', N'B', N'1952-01-03 00:00:00', 1, N'Telephone Psychic', N'2015-01-01 00:00:00', N'2015-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (16, N'Baggins', N'Bilbo                                             ', N'B', N'1952-01-03 00:00:00', 1, N'Master of Bananas', N'2016-01-01 00:00:00', N'2016-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (17, N'Baggins', N'Bilbo                                             ', N'B', N'1952-01-03 00:00:00', 1, N'Lovable Oaf', N'2017-01-01 00:00:00', N'2017-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (18, N'Baggins', N'Bilbo                                             ', N'B', N'1952-01-03 00:00:00', 1, N'Elected Buffoon', N'2018-01-01 00:00:00', N'2018-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (19, N'Baggins', N'Bilbo                                             ', N'B', N'1952-01-03 00:00:00', 1, N'Space Travel Agent', N'2019-01-01 00:00:00', N'2019-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (20, N'Baggins', N'Bilbo                                             ', N'B', N'1952-01-03 00:00:00', 1, N'Intergalactic Surfer', N'2020-01-01 00:00:00', NULL)
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (21, N'Bobbin', N'Babaduk                                           ', N'B', N'1990-08-23 00:00:00', 1, N'Bodhisattva', N'2016-01-01 00:00:00', N'2016-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (22, N'Bobbin', N'Babaduk                                           ', N'B', N'1990-08-23 00:00:00', 1, N'Shortstop', N'2017-01-01 00:00:00', N'2017-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (23, N'Bobbin', N'Babaduk                                           ', N'B', N'1990-08-23 00:00:00', 1, N'Cheif Assitant to the Assistant Cheif', N'2018-01-01 00:00:00', N'2018-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (24, N'Bobbin', N'Babaduk                                           ', N'B', N'1990-08-23 00:00:00', 1, N'Origami Ninja', N'2019-01-01 00:00:00', N'2019-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (25, N'Bobbin', N'Babaduk                                           ', N'B', N'1990-08-23 00:00:00', 1, N'Elected Buffoon', N'2020-01-01 00:00:00', NULL)
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (26, N'Billings', N'Blanche                                           ', N'B', N'1990-05-08 00:00:00', 1, N'Intergalactic Surfer', N'2016-01-01 00:00:00', N'2016-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (27, N'Billings', N'Blanche                                           ', N'B', N'1990-05-08 00:00:00', 1, N'Lovable Oaf', N'2017-01-01 00:00:00', N'2017-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (28, N'Billings', N'Blanche                                           ', N'B', N'1990-05-08 00:00:00', 1, N'Origami Ninja', N'2018-01-01 00:00:00', N'2018-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (29, N'Billings', N'Blanche                                           ', N'B', N'1990-05-08 00:00:00', 1, N'Elected Buffoon', N'2019-01-01 00:00:00', N'2019-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (30, N'Billings', N'Blanche                                           ', N'B', N'1990-05-08 00:00:00', 1, N'Enigmatic Pariah', N'2020-01-01 00:00:00', NULL)
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (31, N'Babigian', N'Bradley                                           ', N'B', N'1977-03-15 00:00:00', 1, N'Elected Buffoon', N'2016-01-01 00:00:00', N'2016-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (32, N'Blazier', N'Blake                                             ', N'B', N'1978-02-01 00:00:00', 0, N'Master of Bananas', N'2016-01-01 00:00:00', N'2016-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (33, N'Blazier', N'Blake                                             ', N'B', N'1978-02-01 00:00:00', 0, N'Shortstop', N'2017-01-01 00:00:00', N'2017-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (34, N'Blazier', N'Blake                                             ', N'B', N'1978-02-01 00:00:00', 0, N'Elected Buffoon', N'2018-01-01 00:00:00', N'2018-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (35, N'Blazier', N'Blake                                             ', N'B', N'1978-02-01 00:00:00', 0, N'Telephone Psychic', N'2019-01-01 00:00:00', N'2019-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (36, N'Blazier', N'Blake                                             ', N'B', N'1978-02-01 00:00:00', 0, N'Intergalactic Surfer', N'2020-01-01 00:00:00', NULL)
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (37, N'Babigian', N'Bradley                                           ', N'B', N'1977-03-15 00:00:00', 1, N'Telephone Psychic', N'2017-01-01 00:00:00', N'2017-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (38, N'Babigian', N'Bradley                                           ', N'B', N'1977-03-15 00:00:00', 1, N'Space Travel Agent', N'2018-01-01 00:00:00', N'2018-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (39, N'Bronchie', N'Betty                                             ', N'B', N'1984-09-08 00:00:00', 0, N'Shortstop', N'2016-01-01 00:00:00', N'2016-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (40, N'Bronchie', N'Betty                                             ', N'B', N'1984-09-08 00:00:00', 0, N'Space Travel Agent', N'2017-01-01 00:00:00', N'2017-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (41, N'Bronchie', N'Betty                                             ', N'B', N'1984-09-08 00:00:00', 0, N'Intergalactic Surfer', N'2018-01-01 00:00:00', N'2018-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (42, N'Bronchie', N'Betty                                             ', N'B', N'1984-09-08 00:00:00', 0, N'Elected Buffoon', N'2019-01-01 00:00:00', N'2019-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (43, N'Bronchie', N'Betty                                             ', N'B', N'1984-09-08 00:00:00', 0, N'Origami Ninja', N'2020-01-01 00:00:00', N'2020-03-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (44, N'Bronchie', N'Betty                                             ', N'B', N'1984-09-08 00:00:00', 0, N'Neurosurgeon', N'2020-06-01 00:00:00', NULL)
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (45, N'Balsey', N'Brian                                             ', N'B', N'1995-08-15 00:00:00', 0, N'Human Cannonball', N'2016-01-01 00:00:00', N'2016-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (46, N'Balsey', N'Brian                                             ', N'B', N'1995-08-15 00:00:00', 0, N'Telephone Psychic', N'2017-01-01 00:00:00', N'2017-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (47, N'Balsey', N'Brian                                             ', N'B', N'1995-08-15 00:00:00', 0, N'Intergalactic Surfer', N'2018-01-01 00:00:00', N'2018-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (48, N'Balsey', N'Brian                                             ', N'B', N'1995-08-15 00:00:00', 0, N'Cheif Assitant to the Assistant Cheif', N'2019-01-01 00:00:00', NULL)
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (49, N'Bobier', N'Basil                                             ', N'B', N'1989-03-15 00:00:00', 0, N'Lovable Oaf', N'2016-01-01 00:00:00', N'2016-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (50, N'Bobier', N'Basil                                             ', N'B', N'1989-03-15 00:00:00', 0, N'Colonel of Mustard', N'2017-01-01 00:00:00', N'2017-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (51, N'Bobier', N'Basil                                             ', N'B', N'1989-03-15 00:00:00', 0, N'Telephone Psychic', N'2018-01-01 00:00:00', N'2018-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (52, N'Bobier', N'Basil                                             ', N'B', N'1989-03-15 00:00:00', 0, N'Intergalactic Surfer', N'2019-01-01 00:00:00', N'2019-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (53, N'Bobier', N'Basil                                             ', N'B', N'1989-03-15 00:00:00', 0, N'Enigmatic Pariah', N'2020-01-01 00:00:00', NULL)
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (54, N'Babigian', N'Bradley                                           ', N'B', N'1977-03-15 00:00:00', 1, N'Master of Bananas', N'2019-01-01 00:00:00', N'2019-06-03 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (55, N'Babigian', N'Bradley                                           ', N'B', N'1977-03-15 00:00:00', 1, N'Shortstop', N'2019-07-01 00:00:00', N'2019-12-31 00:00:00')
INSERT INTO [dbo].[EmployeePositionHistory] ([Id], [LastName], [FirstName], [MiddleInitial], [DateOfBirth], [IsContractor], [Position], [StartDate], [EndDate]) VALUES (56, N'Babigian', N'Bradley                                           ', N'B', N'1977-03-15 00:00:00', 1, N'Origami Ninja', N'2020-01-01 00:00:00', NULL)
SET IDENTITY_INSERT [dbo].[EmployeePositionHistory] OFF
GO

--==================================================
-- STORED PROCEDURES FOR TESTING
--
--  CREATE TABLE
--NonQuery Test Procedure to create secondary test table, used for Commit Testing
CREATE   PROCEDURE [dbo].[spCreate]
	
AS
	BEGIN
	DROP TABLE IF EXISTS [dbo].EmployeePositionHistory2
	
-- The table has all the column types that are supported as parameters by SqlSpClient.
-- NOTE - The table design is not normalized and is only meant to provide simple, convenient data for the stored procedures.
CREATE TABLE [dbo].EmployeePositionHistory2(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[IsContractor] [bit] NULL,
	[Position] [varchar](50) NULL,
	[StartDate] [datetime2](7) NULL,
	[EndDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--====================================
-- DELETE ALL FROM TABLE
--NonQuery Test Procedure deletes secondary test table, used for commit tests
CREATE PROCEDURE [dbo].[spDeleteAll]
AS
	DELETE from dbo.EmployeePositionHistory2
RETURN 0
GO

--=====================================
-- DELETE BY ID
-- Non Query Test Procedure tests parameter type int
CREATE   PROCEDURE [dbo].[spDeleteById]
	@id INT
AS
	BEGIN
	Delete from dbo.EmployeePositionHistory where id=@id;
	END

GO
--=====================================
-- DROP Secondary Table 
-- Non Query Test Procedure 

CREATE   PROCEDURE [dbo].[spDrop]
AS
	DROP TABLE IF EXISTS dbo.EmployeePositionHistory2;


GO

--================================
-- GET BY ALL PARAMETER TYPES
-- Query Test Procedure for all parameter types

Create    PROCEDURE [dbo].[spGetByAll]

	@lastName nvarchar(50),
	@firstName nchar(50),
	@middleInitial char(1),
	@dateOfBirth datetime,
	@isContractor bit,
	@startDate datetime2(7),
	@position varchar(50)

AS

SELECT * FROM dbo.EmployeePositionHistory
	WHERE LastName = @lastName 
	and FirstName = @firstName 
	AND MiddleInitial = @middleInitial
	AND DateOfBirth = @dateOfBirth
	AND IsContractor = @isContractor
	AND StartDate >= @startDate
	AND Position = @position
	ORDER BY LastName, firstName, StartDate;

Return 0
GO

--=========================================================
-- GET BY Id
CREATE  PROCEDURE [dbo].[spGetById]
-- Query Test procedure for parameter type int
	@id int
AS

SELECT * FROM dbo.EmployeePositionHistory
	WHERE id = @id

Return 0;
GO

--==========================================================
--GET BY NAME AND DATE
CREATE  PROCEDURE [dbo].[spGetByNameAndDate]
-- Query Test procedure for parameter types nvarchar and datetime2
	@lastName nvarchar(50),
	@firstName nvarchar(50),
	@middleInitial char(1),
	@startDate datetime2(7),
	@endDate datetime2(7)

AS

SELECT * FROM dbo.EmployeePositionHistory
	WHERE LastName = @lastName and FirstName = @firstName and MiddleInitial = @middleInitial 
	AND StartDate >= @startDate and EndDate <= @endDate   
	ORDER BY LastName, FirstName, StartDate;

	Return 0;
GO

--=============================================================
-- GET BY POSITION
CREATE  PROCEDURE [dbo].[spGetByPosition]
-- Test procedure for parameter type varchar 
	@position varchar(50)

AS

SELECT * FROM dbo.EmployeePositionHistory
	WHERE Position = @position
	ORDER BY LastName, FirstName, StartDate;

	Return 0;

GO

--=======================================
-- GET BY TYPE AND DATE
CREATE  PROCEDURE [dbo].[spGetByTypeAndDate]
-- Test procedure for parameter types nvarchar and datetime2
	@isContractor bit,
	@startDate datetime2(7),
	@endDate datetime2(7)

AS

SELECT * FROM dbo.EmployeePositionHistory
	WHERE IsContractor = @isContractor
	AND StartDate >= @startDate and EndDate <= @endDate   
	ORDER BY LastName, FirstName, StartDate;

Return 0;
GO

--====================================================
-- GET BY TYPE AND MIN DATE OF BIRTH
CREATE  PROCEDURE [dbo].[spGetByTypeAndMinDateOfBirth]
-- Test procedure for parameter types bit and datetime
	@isContractor bit,
	@minDateOfBirth datetime
AS

select * from dbo.EmployeePositionHistory
	where IsContractor = @isContractor and DateOfBirth >= @minDateOfBirth 
	ORDER BY LastName, FirstName, StartDate;

Return 0;
GO

--====================================================================
-- Get CURRENT BY NAME
CREATE  PROCEDURE [dbo].[spGetCurrentByName]
-- Test procedure for parameter types nvarchar and datetime2
	@lastName nvarchar(50),
	@firstName nchar(50),
	@middleInitial char(1)
AS
SELECT * FROM dbo.EmployeePositionHistory
	WHERE LastName = @LastName and FirstName = @FirstName AND MiddleInitial = @middleInitial
	AND StartDate IS NOT NULL
	and EndDate IS NULL;
Return 0;
GO

--======================================================================================
-- INSERT Single Record
CREATE PROCEDURE spInsert
AS
BEGIN
SET IDENTITY_INSERT dbo.EmployeePositionHistory ON
INSERT INTO EmployeePositionHistory (id, [LastName] , [FirstName], [DateOfBirth], [IsContractor] , [Position] , [StartDate], [EndDate] )
VALUES
(57, N'Bishop', N'Briana', N'1968-01-02T00:00:00', 0, 'Intergalactic Surfer', N'2018-03-01T00:00:00', N'2018-12-24T00:00:00' )

SET IDENTITY_INSERT dbo.EmployeePositionHistory OFF
END
GO

--======================================================================================
-- GENERATE PROCEDURE ERROR FOR TEST (1)
CREATE    PROCEDURE [dbo].[spProcError1]
AS
 DECLARE @id INT
  set @id =1/0;
RETURN 0
GO

--=======================================================================================
-- GENERATE PROCEDURE ERROR FOR TEST (2)
CREATE PROCEDURE [dbo].[spProcError2]
AS
EXEC ('UPDATE notable SET nocolumn = null')
GO

--===========================================================================================
--UPDATE SINGLE RECORD
-- Non Query test procedure for Parameter types Int and Char
CREATE   PROCEDURE [dbo].[spUpdateMiddleInitial]
	@id int,
	@middleInitial char(10)
AS
	BEGIN
UPDATE dbo.EmployeePositionHistory  SET MiddleInitial = @middleInitial WHERE Id =@id;
	END
GO

--================================================================================
-- WAIT FOR SECONDS
-- Non Query test procedure for parameter type int
CREATE PROCEDURE [dbo].[spWaitForSeconds]
	@seconds int
AS
DECLARE @delay DATETIME
SELECT @delay = DATEADD(SECOND,@seconds, CONVERT(DATETIME,0))
	
	WAITFOR	DELAY @delay

RETURN 0

GO