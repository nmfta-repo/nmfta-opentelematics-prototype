CREATE TABLE [dbo].[USHwyDataPoints]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [NodeId] INT NULL,
	[HwySectionName] NVARCHAR(50) NULL, 
    [Order] INT NULL, 
    [Longitude] DECIMAL(18, 5) NULL, 
    [Latitude] DECIMAL(18, 5) NULL, 

)
