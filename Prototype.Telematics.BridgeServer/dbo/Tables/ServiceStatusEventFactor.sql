﻿CREATE TABLE [dbo].[ServiceStatusEventFactor]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [eventId] UNIQUEIDENTIFIER NOT NULL, 
    [Factor] NVARCHAR(1000) NOT NULL
)
