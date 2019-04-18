CREATE TABLE [dbo].[StopGeographicDetails]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [routeId] UNIQUEIDENTIFIER NOT NULL,    
    [stopName] NVARCHAR(200) NULL, 
    [address] NVARCHAR(500) NULL, 
    [comment] NVARCHAR(500) NULL, 
    [longitude] NUMERIC(18, 8) NULL, 
    [latitude] NUMERIC(18, 8) NULL, 
    [stopDeadline] DATETIMEOFFSET NULL,
    [entryArea] NVARCHAR(MAX) NULL
)
