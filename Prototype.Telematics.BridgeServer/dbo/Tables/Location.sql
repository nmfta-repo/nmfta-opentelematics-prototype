CREATE TABLE [dbo].[Location]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [latitude] NUMERIC(18, 8) NOT NULL, 
    [longitude] NUMERIC(18, 8) NOT NULL, 
    [identifiedPlace] NVARCHAR(100) NULL, 
    [identifiedState] NVARCHAR(50) NULL, 
    [distanceFrom] NUMERIC(18, 3) NULL, 
    [directionFrom] NVARCHAR(10) NULL
)
