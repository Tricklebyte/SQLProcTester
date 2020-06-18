CREATE PROCEDURE [dbo].[spCreate]
	
AS
	
-- The table has all the column types that are supported as parameters by SqlSpClient.
-- NOTE - The table design is not normalized and is only meant to provide simple, convenient data for the stored procedures.
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

Return 1