CREATE TABLE [dbo].[DutyStatusLog]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [dateTime] DATETIMEOFFSET NOT NULL, 
    [vehicleId] UNIQUEIDENTIFIER NOT NULL, 
    [driverId] UNIQUEIDENTIFIER NOT NULL, 
    [distanceLastValid] NUMERIC(18, 5) NOT NULL, 
    [editDateTime] DATETIMEOFFSET NULL, 
    [eventRecordStatus] NVARCHAR(100) NOT NULL, 
    [eventType] NVARCHAR(100) NOT NULL, 
    [latitude] NUMERIC(18, 8) NOT NULL, 
    [longitude] NUMERIC(18, 8) NOT NULL, 
    [malfunction] NVARCHAR(100) NOT NULL, 
    [origin] NVARCHAR(100) NOT NULL, 
    [parentId] UNIQUEIDENTIFIER NULL, 
    [sequence] INT NOT NULL, 
    [state] NVARCHAR(100) NOT NULL, 
    [status] NVARCHAR(100) NOT NULL, 
    [verifyDateTime] DATETIMEOFFSET NULL, 
    [multidayBasis] INT NOT NULL, 
    [outputFileComment] NVARCHAR(1000) NOT NULL, 
    [eventDataChecksum] NVARCHAR(100) NULL, 
    [locationId] UNIQUEIDENTIFIER NULL
)
