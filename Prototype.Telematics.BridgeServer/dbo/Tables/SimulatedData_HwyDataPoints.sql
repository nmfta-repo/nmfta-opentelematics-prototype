CREATE TABLE [dbo].[SimulatedData_HwyDataPoints]
(
	[Id] INT IDENTITY(1000,1) NOT NULL PRIMARY KEY,  
    [NodeId] NVARCHAR(50)  NULL,
	[HwySectionName] NVARCHAR(50) NULL, 
    [Sequence] INT NULL, 
    [Longitude] DECIMAL(18, 8) NULL, 
    [Latitude] DECIMAL(18, 8) NULL, 
    [Direction] NVARCHAR(10) NULL, 
)
