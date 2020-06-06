USE [SqlProcTest]
GO

/****** Object: SqlProcedure [dbo].[spGetCurrentPositionById] Script Date: 5/27/2020 5:15:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Test procedure for parameter type int
CREATE OR ALTER PROCEDURE [dbo].[spGetPositionHistoryById]
	@Id int
AS
SELECT * FROM dbo.EmployeePositionHistory
	WHERE id = @Id
	ORDER BY LastName, FirstName, StartDate;
	GO

--==========================================================
-- Test procedure for parameter types nvarchar 	
CREATE OR ALTER PROCEDURE [dbo].[spGetCurrentPositionByName]

	@LastName nvarchar(50),
	@FirstName nvarchar(50)
AS
SELECT * FROM dbo.EmployeePositionHistory
	WHERE LastName = @LastName and FirstName = @FirstName 
	AND StartDate IS NOT NULL
	and EndDate IS NULL
	ORDER BY LastName, FirstName, StartDate;
	GO


--===========================================================
-- Test procedure for parameter types nvarchar and datetime2	
CREATE OR ALTER PROCEDURE [dbo].[spGetHistoryByNameAndDate]
	@LastName nvarchar(50),
	@FirstName nvarchar(50),
	@StartDate datetime2(7),
	@EndDate datetime2(7)
AS

SELECT * FROM dbo.EmployeePositionHistory
	WHERE LastName = @LastName and FirstName = @FirstName 
	AND StartDate >= @StartDate and EndDate <= @EndDate   
	ORDER BY LastName, FirstName, StartDate;
GO


--==========================================================
-- Test procedure for parameter type varchar 
CREATE OR ALTER PROCEDURE [dbo].[spGetHistoryByPosition]
	@position varchar(50)
AS
SELECT * FROM dbo.EmployeePositionHistory
	WHERE Position = @position
	ORDER BY LastName, FirstName, StartDate;
	GO;

--==========================================================
-- Test procedure for parameter types bit and datetime2	
CREATE OR ALTER PROCEDURE [dbo].[spGetHistoryByTypeAndDate]

	@IsContractor bit,
	@StartDate datetime2(7),
	@EndDate datetime2(7)

AS

SELECT * FROM dbo.EmployeePositionHistory
	WHERE IsContractor = @IsContractor
	AND StartDate >= @StartDate and EndDate <= @EndDate   
	ORDER BY LastName, FirstName, StartDate;

	GO


--====================================================
-- Test procedure for parameter types bit and datetime	
CREATE OR ALTER PROCEDURE [dbo].[spGetHistoryByTypeAndMinDOB]

	@IsContractor bit,
	@MinDOB datetime
AS

select * from dbo.EmployeePositionHistory
	where IsContractor = @IsContractor and DateOfBirth > @MinDOB 
	ORDER BY LastName, FirstName, StartDate;

	GO
--=======================================================

CREATE  PROCEDURE [dbo].[spGetHistoryByAll]
-- Test procedure for all supported parameter types
	@LastName nvarchar(50),
	@FirstName nvarchar(50),
	@DOB datetime,
	@IsContractor bit,
	@StartDate datetime2(7),
	@Position varchar(50)

AS

SELECT * FROM dbo.EmployeePositionHistory
	WHERE LastName = @LastName and FirstName = @FirstName 
	AND DateOfBirth = @DOB
	AND IsContractor = @IsContractor
	AND StartDate >= @StartDate
	AND Position = @Position
	ORDER BY LastName, FirstName, StartDate;
