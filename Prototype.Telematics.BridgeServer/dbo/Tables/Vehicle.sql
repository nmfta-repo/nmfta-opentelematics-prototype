CREATE TABLE [dbo].[Vehicle]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [providerId] nvarchar(100) NULL, 
    [name] NVARCHAR(100) NULL, 
    [cmvVIN] NVARCHAR(100) NULL, 
    [licensePlate] NVARCHAR(50) NULL
)
