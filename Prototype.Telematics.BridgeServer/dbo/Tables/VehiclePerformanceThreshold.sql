CREATE TABLE [dbo].[VehiclePerformanceThreshold]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [providerId] NVARCHAR(100) NULL, 
    [activeFrom] DATETIMEOFFSET NULL, 
    [activeTo] DATETIMEOFFSET NULL, 
    [rpmOverValue] INT NULL, 
    [overSeedValue] INT NULL, 
    [excessSpeedValue] INT NULL, 
    [longIdleValue] INT NULL, 
    [hiThrottleValue] INT NULL
)
