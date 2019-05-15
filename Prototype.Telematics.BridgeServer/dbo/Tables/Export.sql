CREATE TABLE [dbo].[Export]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [export_date] DATE NOT NULL, 
	[export_type] nvarchar(25) NOT NULL DEFAULT N'Full',
    [location] NVARCHAR(500) NOT NULL 
)

GO

CREATE INDEX [IX_Export_Date] ON [dbo].[Export] ([export_date] DESC)
