CREATE TABLE [dbo].[DutyStatusChange]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [driverId] UNIQUEIDENTIFIER NOT NULL, 
    [dateTime] DATETIMEOFFSET NOT NULL, 
    [latitude] NUMERIC(18, 8) NOT NULL, 
    [longitude] NUMERIC(18, 8) NOT NULL, 
    [status] NVARCHAR(100) NOT NULL, 
)
