CREATE TABLE [dbo].[StopGeographicDetails]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [providerId] NVARCHAR(100) NULL, 
    [stopName] NVARCHAR(200) NULL, 
    [address] NVARCHAR(500) NULL, 
    [comment] NVARCHAR(500) NULL, 
    [longitude] NUMERIC(18, 8) NULL, 
    [latitude] NUMERIC(18, 8) NULL, 
    [entryArea] NVARCHAR(MAX) NULL
)
