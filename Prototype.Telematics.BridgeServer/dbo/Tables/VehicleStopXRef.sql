﻿CREATE TABLE [dbo].[VehicleStopXRef]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [vehicleId] UNIQUEIDENTIFIER NOT NULL, 
    [stopId] UNIQUEIDENTIFIER NOT NULL, 
    [createdOn] DATETIMEOFFSET NOT NULL
)
