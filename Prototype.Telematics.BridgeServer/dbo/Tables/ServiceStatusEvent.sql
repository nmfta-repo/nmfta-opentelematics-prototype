﻿CREATE TABLE [dbo].[ServiceStatusEvent]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [Status] NVARCHAR(100) NOT NULL, 
    [dateTime] DATETIMEOFFSET NOT NULL
)
