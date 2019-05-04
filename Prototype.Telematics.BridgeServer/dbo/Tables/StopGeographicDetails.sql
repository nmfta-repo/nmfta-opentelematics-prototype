CREATE TABLE [dbo].[StopGeographicDetails]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [routeId] UNIQUEIDENTIFIER NULL,    
    [stopName] NVARCHAR(200) NOT NULL, 
    [address] NVARCHAR(500) NULL, 
    [comment] NVARCHAR(500) NULL, 
    [longitude] NUMERIC(18, 8) NOT NULL, 
    [latitude] NUMERIC(18, 8) NOT NULL, 
    [stopDeadline] DATETIMEOFFSET NULL,
    [entryArea] NVARCHAR(MAX) NULL
)
