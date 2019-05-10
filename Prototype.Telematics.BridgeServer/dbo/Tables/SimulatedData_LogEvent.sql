CREATE TABLE [dbo].[SimulatedData_LogEvent]
(
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[driverId] [int] NULL,
	[coDrivers] [nvarchar](1000) NULL,
	[distanceLastValid] [numeric](18, 5) NOT NULL,
	[editDateTime] [datetimeoffset](7) NULL,
	[locationId] [uniqueidentifier] NOT NULL,
	[origin] [nvarchar](100) NOT NULL,
	[parentId] [uniqueidentifier] NULL,
	[sequence] [int] NOT NULL,
	[state] [nvarchar](100) NOT NULL,
	[eventType] [nvarchar](100) NOT NULL,
	[certificationCount] [int] NULL,
	[verifyDateTime] [datetimeoffset](7) NULL,
	[multidayBasis] [int] NOT NULL,
	[comment] [nvarchar](1000) NOT NULL,
	[eventDataChecksum] [nvarchar](100) NOT NULL,
	[createdOrder] [int] NULL
)
