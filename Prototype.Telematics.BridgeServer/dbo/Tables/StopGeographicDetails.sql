CREATE TABLE [dbo].[StopGeographicDetails]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [providerId] UNIQUEIDENTIFIER NULL, 
    [stopName] NVARCHAR(250) NULL, 
    [address] NVARCHAR(250) NULL, 
    [comment] NVARCHAR(250) NULL, 
    [longitude] NUMERIC(18, 8) NULL, 
    [latitude] NUMERIC(18, 8) NULL, 
    [entryArea] NVARCHAR(MAX) NULL
)
