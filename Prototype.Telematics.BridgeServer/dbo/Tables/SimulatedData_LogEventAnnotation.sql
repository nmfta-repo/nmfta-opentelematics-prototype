CREATE TABLE [dbo].[SimulatedData_LogEventAnnotation]
(
	[Id] INT IDENTITY(1000,1) NOT NULL PRIMARY KEY,
    [logEventId] INT NOT NULL, 
    [driverId] UNIQUEIDENTIFIER NULL, 
    [comment] NVARCHAR(4000) NULL, 
    [dateTime] DATETIMEOFFSET NULL
)