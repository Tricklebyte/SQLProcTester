USE [SqlProcTest]
GO


/****** Object: SqlProcedure [dbo].[spGetByAll] Script Date: 6/7/2020 2:17:54 PM ******/
CREATE  PROCEDURE [dbo].[spGetByAll]
-- Test procedure for parameter types nvarchar and datetime2
	@lastName nvarchar(50),
	@firstName nvarchar(50),
	@dateOfBirth datetime,
	@isContractor bit,
	@startDate datetime2(7),
	@position varchar(50)

AS

SELECT * FROM dbo.EmployeePositionHistory
	WHERE LastName = @lastName and FirstName = @firstName 
	AND DateOfBirth = @dateOfBirth
	AND IsContractor = @isContractor
	AND StartDate >= @startDate
	AND Position = @position
	ORDER BY LastName, firstName, StartDate;

Return 0
GO

--===============================================================
/****** Object: SqlProcedure [dbo].[spGetById] Script Date: 6/7/2020 2:17:12 PM ******/
CREATE  PROCEDURE [dbo].[spGetById]
-- Test procedure for parameter type int
	@id int
AS


SELECT * FROM dbo.EmployeePositionHistory
	WHERE id = @id

Return 0;

GO;

--===============================================================
/****** Object: SqlProcedure [dbo].[spGetByNameAndDate] Script Date: 6/7/2020 2:15:29 PM ******/
CREATE  PROCEDURE [dbo].[spGetByNameAndDate]
-- Test procedure for parameter types nvarchar and datetime2
	@lastName nvarchar(50),
	@firstName nvarchar(50),
	@startDate datetime2(7),
	@endDate datetime2(7)

AS

SELECT * FROM dbo.EmployeePositionHistory
	WHERE LastName = @lastName and FirstName = @firstName 
	AND StartDate >= @startDate and EndDate <= @endDate   
	ORDER BY LastName, FirstName, StartDate;

	Return 0;
GO

--===============================================================
/****** Object: SqlProcedure [dbo].[spGetByPosition] Script Date: 6/7/2020 2:16:03 PM ******/

CREATE  PROCEDURE [dbo].[spGetByPosition]
-- Test procedure for parameter type varchar 
	@position varchar(50)

AS

SELECT * FROM dbo.EmployeePositionHistory
	WHERE Position = @position
	ORDER BY LastName, FirstName, StartDate;

	Return 0;
GO

USE [SqlProcTest]
GO

--=======================================================================
/****** Object: SqlProcedure [dbo].[spGetByTypeAndDate] Script Date: 6/7/2020 2:19:52 PM ******/

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
GO;

--=================================================================
/****** Object: SqlProcedure [dbo].[spGetByTypeAndMinDateOfBirth] Script Date: 6/7/2020 2:20:40 PM ******/

CREATE  PROCEDURE [dbo].[spGetByTypeAndMinDateOfBirth]
-- Test procedure for parameter types bit and datetime
	@isContractor bit,
	@minDateOfBirth datetime
AS

select * from dbo.EmployeePositionHistory
	where IsContractor = @isContractor and DateOfBirth >= @minDateOfBirth 
	ORDER BY LastName, FirstName, StartDate;

Return 0;
GO;

--========================================================================
/****** Object: SqlProcedure [dbo].[spGetCurrentByName] Script Date: 6/7/2020 2:21:12 PM ******/

CREATE  PROCEDURE [dbo].[spGetCurrentByName]
-- Test procedure for parameter types nvarchar and datetime2
	@LastName nvarchar(50),
	@FirstName nvarchar(50)

AS

SELECT * FROM dbo.EmployeePositionHistory
	WHERE LastName = @LastName and FirstName = @FirstName 
	AND StartDate IS NOT NULL
	and EndDate IS NULL;

Return 0;
GO;

 --===========================================================================
/****** Object: SqlProcedure [dbo].[spWaitForSeconds] Script Date: 6/7/2020 2:22:17 PM ******/

CREATE PROCEDURE [dbo].[spWaitForSeconds]
	@seconds int
AS
DECLARE @delay DATETIME
SELECT @delay = DATEADD(SECOND,@seconds, CONVERT(DATETIME,0))
	
	WAITFOR	DELAY @delay

RETURN 0
