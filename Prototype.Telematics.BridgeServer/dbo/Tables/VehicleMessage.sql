﻿CREATE TABLE [dbo].[VehicleMessage]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [vehicleId] UNIQUEIDENTIFIER NOT NULL, 
    [subject] NVARCHAR(500) NOT NULL, 
    [message] NVARCHAR(4000) NOT NULL, 
    [displayAt] DATETIMEOFFSET NOT NULL,
    [createdOn] DATETIMEOFFSET NOT NULL 
)