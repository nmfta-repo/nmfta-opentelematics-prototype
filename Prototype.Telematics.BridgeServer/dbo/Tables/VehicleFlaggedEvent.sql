CREATE TABLE [dbo].[VehicleFlaggedEvent]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [eventStart] DATETIMEOFFSET NOT NULL, 
    [eventEnd] DATETIMEOFFSET NOT NULL, 
    [vehicleId] UNIQUEIDENTIFIER NOT NULL, 
    [driverId] UNIQUEIDENTIFIER NOT NULL, 
    [eventComment] NVARCHAR(1000) NULL, 
    [trigger] NVARCHAR(100) NOT NULL, 
    [gpsSpeed] NUMERIC(18, 5) NULL, 
    [gpsHeading] NUMERIC(18, 5) NULL, 
    [gpsQuality] NUMERIC(18, 5) NULL, 
    [ecmSpeed] NUMERIC(18, 5) NULL, 
    [engineRPM] NUMERIC(18, 5) NULL, 
    [accelerationPercent] NUMERIC(18, 5) NULL, 
    [seatBelts] BIT NOT NULL, 
    [cruiseStatus] NVARCHAR(MAX) NOT NULL, 
    [parkingBreak] BIT NOT NULL, 
    [ignitionStatus] NVARCHAR(MAX) NOT NULL, 
    [forwardVehicleSpeed] NUMERIC(18, 5) NULL, 
    [forwardVehicleDistance] NUMERIC(18, 5) NULL, 
    [forwardVehicleElapsed] NUMERIC(18, 5) NULL, 
    [odometer] NUMERIC(18, 5) NULL
)
