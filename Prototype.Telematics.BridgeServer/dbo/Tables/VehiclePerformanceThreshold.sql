CREATE TABLE [dbo].[VehiclePerformanceThreshold]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [activeFrom] DATETIMEOFFSET NULL, 
    [activeTo] DATETIMEOFFSET NULL, 
    [rpmOverValue] INT NULL, 
    [overSpeedValue] INT NULL, 
    [excessSpeedValue] INT NULL, 
    [longIdleValue] INT NULL, 
    [hiThrottleValue] INT NULL
)
