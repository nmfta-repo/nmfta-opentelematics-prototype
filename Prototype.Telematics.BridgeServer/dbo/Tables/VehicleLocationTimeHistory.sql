CREATE TABLE [dbo].[VehicleLocationTimeHistory]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [vehicleId] UNIQUEIDENTIFIER NOT NULL, 
    [driverId] UNIQUEIDENTIFIER NOT NULL, 
    [dateTime] DATETIMEOFFSET NOT NULL, 
    [latitude] NUMERIC(18, 8) NOT NULL, 
    [longitude] NUMERIC(18, 8) NOT NULL,
	[sequence] BIGINT IDENTITY(1000, 1) NOT NULL, 
    CONSTRAINT [FK_VehicleLocationTimeHistory_Vehicle] FOREIGN KEY ([vehicleId]) REFERENCES [Vehicle]([Id])
)
