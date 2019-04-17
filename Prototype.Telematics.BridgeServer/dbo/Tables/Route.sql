CREATE TABLE [dbo].[Route]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [routeName] NVARCHAR(100) NULL, 
    [startName] NVARCHAR(200) NOT NULL, 
    [startAddress] NVARCHAR(500) NULL, 
    [startLatitude] DECIMAL(18, 8) NOT NULL, 
    [startLongitude] DECIMAL(18, 8) NOT NULL, 
    [comment] NVARCHAR(500) NULL
)
