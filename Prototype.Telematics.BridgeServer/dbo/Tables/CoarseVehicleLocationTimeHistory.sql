CREATE TABLE [dbo].[CoarseVehicleLocationTimeHistory]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [vehicleId] UNIQUEIDENTIFIER NOT NULL, 
    [driverId] UNIQUEIDENTIFIER NOT NULL, 
    [dateTime] DATETIMEOFFSET NOT NULL, 
    [latitude] NUMERIC(18, 8) NOT NULL, 
    [longitude] NUMERIC(18, 8) NOT NULL
)
