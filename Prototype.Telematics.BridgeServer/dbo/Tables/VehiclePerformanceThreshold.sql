CREATE TABLE [dbo].[VehiclePerformanceThreshold]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [activeFrom] DATETIMEOFFSET NOT NULL, 
    [activeTo] DATETIMEOFFSET NULL, 
    [rpmOverValue] INT NULL, 
    [overSpeedValue] INT NULL, 
    [excessSpeedValue] INT NULL, 
    [longIdleValue] INT NULL, 
    [hiThrottleValue] INT NULL
)
