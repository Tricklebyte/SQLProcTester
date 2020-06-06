USE [SqlProcTest]
GO

/****** Object: SqlProcedure [dbo].[spGetHistoryByAll] Script Date: 5/27/2020 6:41:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


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
