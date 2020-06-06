USE [SqlProcTest]
GO

/****** Object:  Table [dbo].[EmployeePositionHistory]    Script Date: 5/26/2020 7:41:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
DROP TABLE IF EXISTS dbo.EmployeePositionHistory
GO

CREATE TABLE [dbo].[EmployeePositionHistory](
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
GO

SET IDENTITY_INSERT EmployeePositionHistory ON
GO
INSERT INTO EmployeePositionHistory ([Id] , [LastName] , [FirstName], [DateOfBirth], [IsContractor] , [Position] , [StartDate], [EndDate] )

VALUES
( 1, N'Barker', N'Bob', N'1968-01-02T00:00:00', 1, 'Master of Bananas', N'2018-01-01T00:00:00', N'2018-07-24T00:00:00' ), 
( 2, N'Barker', N'Bob', N'1968-01-02T00:00:00', 1, 'Space Travel Agent', N'2018-07-25T00:00:00', N'2019-03-05T00:00:00' ), 
( 3, N'Barker', N'Bob', N'1968-01-02T00:00:00', 1, 'Bodhisattva', N'2020-02-03T00:00:00', NULL ), 
( 4, N'Baker', N'Bill', N'1972-02-03T00:00:00', 0, 'Cheif Assitant to the Assistant Cheif', N'2010-05-01T00:00:00', N'2012-08-26T00:00:00' ), 
( 5, N'Baker', N'Bill', N'1972-02-03T00:00:00', 0, 'Telephone Psychic', N'2016-08-27T00:00:00', N'2019-07-07T00:00:00' ), 
( 6, N'Baker', N'Bill', N'1972-02-03T00:00:00', 0, 'Intergalactic Surfer', N'2019-07-08T00:00:00', NULL ), 
( 7, N'Barkley', N'Bart', N'1980-11-12T00:00:00', 0, 'Origami Ninja', N'2010-01-02T00:00:00', N'2015-02-02T00:00:00' ), 
( 8, N'Barkley', N'Bart', N'1980-11-12T00:00:00', 0, 'Elected Buffoon', N'2015-02-03T00:00:00', N'2018-02-03T00:00:00' ), 
( 9, N'Barkley', N'Bart', N'1980-11-12T00:00:00', 0, 'Colonel of Mustard', N'2019-07-04T00:00:00', N'2020-03-02T00:00:00' ), 
( 10, N'Barkley', N'Bart', N'1980-11-12T00:00:00', 0, 'Human Cannonball', N'2020-05-01T00:00:00', NULL ), 
( 11, N'Bacon', N'Brady', N'1990-09-12T00:00:00', 0, 'Lovable Oaf', N'2001-02-03T00:00:00', N'2005-02-25T00:00:00' ), 
( 12, N'Bacon', N'Brady', N'1990-09-12T00:00:00', 0, 'Telephone Psychic', N'2005-02-26T00:00:00', N'2013-06-08T00:00:00' ), 
( 13, N'Bacon', N'Brady', N'1990-09-12T00:00:00', 0, 'Human Cannonball', N'2013-06-09T00:00:00', N'2019-12-12T00:00:00' ), 
( 14, N'Bacon', N'Brady', N'1990-09-12T00:00:00', 0, 'Space Travel Agent', N'2019-12-13T00:00:00', N'2020-01-01T00:00:00' ), 
( 15, N'Baggins', N'Bilbo', N'1952-01-03T00:00:00', 1, 'Telephone Psychic', N'2015-01-01T00:00:00', N'2015-12-31T00:00:00' ), 
( 16, N'Baggins', N'Bilbo', N'1952-01-03T00:00:00', 1, 'Master of Bananas', N'2016-01-01T00:00:00', N'2016-12-31T00:00:00' ), 
( 17, N'Baggins', N'Bilbo', N'1952-01-03T00:00:00', 1, 'Lovable Oaf', N'2017-01-01T00:00:00', N'2017-12-31T00:00:00' ), 
( 18, N'Baggins', N'Bilbo', N'1952-01-03T00:00:00', 1, 'Elected Buffoon', N'2018-01-01T00:00:00', N'2018-12-31T00:00:00' ), 
( 19, N'Baggins', N'Bilbo', N'1952-01-03T00:00:00', 1, 'Space Travel Agent', N'2019-01-01T00:00:00', N'2019-12-31T00:00:00' ), 
( 20, N'Baggins', N'Bilbo', N'1952-01-03T00:00:00', 1, 'Intergalactic Surfer', N'2020-01-01T00:00:00', NULL ), 
( 21, N'Bobbin', N'Babaduk', N'1990-08-23T00:00:00', 1, 'Bodhisattva', N'2016-01-01T00:00:00', N'2016-12-31T00:00:00' ), 
( 22, N'Bobbin', N'Babaduk', N'1990-08-23T00:00:00', 1, 'Shortstop', N'2017-01-01T00:00:00', N'2017-12-31T00:00:00' ), 
( 23, N'Bobbin', N'Babaduk', N'1990-08-23T00:00:00', 1, 'Cheif Assitant to the Assistant Cheif', N'2018-01-01T00:00:00', N'2018-12-31T00:00:00' ), 
( 24, N'Bobbin', N'Babaduk', N'1990-08-23T00:00:00', 1, 'Origami Ninja', N'2019-01-01T00:00:00', N'2019-12-31T00:00:00' ), 
( 25, N'Bobbin', N'Babaduk', N'1990-08-23T00:00:00', 1, 'Elected Buffoon', N'2020-01-01T00:00:00', NULL ), 
( 26, N'Billings', N'Blanche', N'1990-05-08T00:00:00', 1, 'Intergalactic Surfer', N'2016-01-01T00:00:00', N'2016-12-31T00:00:00' ), 
( 27, N'Billings', N'Blanche', N'1990-05-08T00:00:00', 1, 'Lovable Oaf', N'2017-01-01T00:00:00', N'2017-12-31T00:00:00' ), 
( 28, N'Billings', N'Blanche', N'1990-05-08T00:00:00', 1, 'Origami Ninja', N'2018-01-01T00:00:00', N'2018-12-31T00:00:00' ), 
( 29, N'Billings', N'Blanche', N'1990-05-08T00:00:00', 1, 'Elected Buffoon', N'2019-01-01T00:00:00', N'2019-12-31T00:00:00' ), 
( 30, N'Billings', N'Blanche', N'1990-05-08T00:00:00', 1, 'Enigmatic Pariah', N'2020-01-01T00:00:00', NULL ), 
( 31, N'Babigian', N'Bradley', N'1977-03-15T00:00:00', 1, 'Elected Buffoon', N'2016-01-01T00:00:00', N'2016-12-31T00:00:00' ), 
( 32, N'Blazier', N'Blake', N'1978-02-01T00:00:00', 0, 'Master of Bananas', N'2016-01-01T00:00:00', N'2016-12-31T00:00:00' ), 
( 33, N'Blazier', N'Blake', N'1978-02-01T00:00:00', 0, 'Shortstop', N'2017-01-01T00:00:00', N'2017-12-31T00:00:00' ), 
( 34, N'Blazier', N'Blake', N'1978-02-01T00:00:00', 0, 'Elected Buffoon', N'2018-01-01T00:00:00', N'2018-12-31T00:00:00' ), 
( 35, N'Blazier', N'Blake', N'1978-02-01T00:00:00', 0, 'Telephone Psychic', N'2019-01-01T00:00:00', N'2019-12-31T00:00:00' ), 
( 36, N'Blazier', N'Blake', N'1978-02-01T00:00:00', 0, 'Intergalactic Surfer', N'2020-01-01T00:00:00', NULL ), 
( 37, N'Babigian', N'Bradley', N'1977-03-15T00:00:00', 1, 'Telephone Psychic', N'2017-01-01T00:00:00', N'2017-12-31T00:00:00' ), 
( 38, N'Babigian', N'Bradley', N'1977-03-15T00:00:00', 1, 'Space Travel Agent', N'2018-01-01T00:00:00', N'2018-12-31T00:00:00' ), 
( 39, N'Bronchie', N'Betty', N'1984-09-08T00:00:00', 0, 'Shortstop', N'2016-01-01T00:00:00', N'2016-12-31T00:00:00' ), 
( 40, N'Bronchie', N'Betty', N'1984-09-08T00:00:00', 0, 'Space Travel Agent', N'2017-01-01T00:00:00', N'2017-12-31T00:00:00' ), 
( 41, N'Bronchie', N'Betty', N'1984-09-08T00:00:00', 0, 'Intergalactic Surfer', N'2018-01-01T00:00:00', N'2018-12-31T00:00:00' ), 
( 42, N'Bronchie', N'Betty', N'1984-09-08T00:00:00', 0, 'Elected Buffoon', N'2019-01-01T00:00:00', N'2019-12-31T00:00:00' ), 
( 43, N'Bronchie', N'Betty', N'1984-09-08T00:00:00', 0, 'Origami Ninja', N'2020-01-01T00:00:00', N'2020-03-31T00:00:00' ), 
( 44, N'Bronchie', N'Betty', N'1984-09-08T00:00:00', 0, 'Neurosurgeon', N'2020-06-01T00:00:00', NULL ), 
( 45, N'Balsey', N'Brian', N'1995-08-15T00:00:00', 0, 'Human Cannonball', N'2016-01-01T00:00:00', N'2016-12-31T00:00:00' ), 
( 46, N'Balsey', N'Brian', N'1995-08-15T00:00:00', 0, 'Telephone Psychic', N'2017-01-01T00:00:00', N'2017-12-31T00:00:00' ), 
( 47, N'Balsey', N'Brian', N'1995-08-15T00:00:00', 0, 'Intergalactic Surfer', N'2018-01-01T00:00:00', N'2018-12-31T00:00:00' ), 
( 48, N'Balsey', N'Brian', N'1995-08-15T00:00:00', 0, 'Cheif Assitant to the Assistant Cheif', N'2019-01-01T00:00:00', NULL ), 
( 49, N'Bobier', N'Basil', N'1989-03-15T00:00:00', 0, 'Lovable Oaf', N'2016-01-01T00:00:00', N'2016-12-31T00:00:00' ), 
( 50, N'Bobier', N'Basil', N'1989-03-15T00:00:00', 0, 'Colonel of Mustard', N'2017-01-01T00:00:00', N'2017-12-31T00:00:00' ), 
( 51, N'Bobier', N'Basil', N'1989-03-15T00:00:00', 0, 'Telephone Psychic', N'2018-01-01T00:00:00', N'2018-12-31T00:00:00' ), 
( 52, N'Bobier', N'Basil', N'1989-03-15T00:00:00', 0, 'Intergalactic Surfer', N'2019-01-01T00:00:00', N'2019-12-31T00:00:00' ), 
( 53, N'Bobier', N'Basil', N'1989-03-15T00:00:00', 0, 'Enigmatic Pariah', N'2020-01-01T00:00:00', NULL ), 
( 54, N'Babigian', N'Bradley', N'1977-03-15T00:00:00', 1, 'Master of Bananas', N'2019-01-01T00:00:00', N'2019-06-03T00:00:00' ), 
( 55, N'Babigian', N'Bradley', N'1977-03-15T00:00:00', 1, 'Shortstop', N'2019-07-01T00:00:00', N'2019-12-31T00:00:00' ), 
( 56, N'Babigian', N'Bradley', N'1977-03-15T00:00:00', 1, 'Origami Ninja', N'2020-01-01T00:00:00', NULL )
SET IDENTITY_INSERT EmployeePositionHistory OFF
GO