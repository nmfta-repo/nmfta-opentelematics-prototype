CREATE TABLE [dbo].[DriverWorkLog]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [driverId] UNIQUEIDENTIFIER NOT NULL, 
    [workDate] DATE NOT NULL, 
    [hoursWorked] DECIMAL(18, 4) NULL
)
