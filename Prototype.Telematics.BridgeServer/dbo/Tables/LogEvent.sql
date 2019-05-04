CREATE TABLE [dbo].[LogEvent]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [dateTime] DATETIMEOFFSET NOT NULL, 
    [vehicleId] UNIQUEIDENTIFIER NOT NULL, 
    [driverId] UNIQUEIDENTIFIER NOT NULL,
	[coDrivers] NVARCHAR(1000) NULL,
    [distanceLastValid] NUMERIC(18, 5) NOT NULL, 
    [editDateTime] DATETIMEOFFSET NULL, 
    [locationId] UNIQUEIDENTIFIER NOT NULL,
    [origin] NVARCHAR(100) NOT NULL, 
    [parentId] UNIQUEIDENTIFIER NULL, 
    [sequence] INT NOT NULL, 
    [state] NVARCHAR(100) NOT NULL, 
    [eventType] NVARCHAR(100) NOT NULL, 
	[certificationCount] int NULL,
    [verifyDateTime] DATETIMEOFFSET NULL, 
    [multidayBasis] INT NOT NULL, 
    [comment] NVARCHAR(1000) NOT NULL, 
    [eventDataChecksum] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_LogEvent_Location] FOREIGN KEY ([locationId]) REFERENCES [Location]([Id])
)
