CREATE TABLE [dbo].[LogEventAnnotation]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
	[logEventId] UNIQUEIDENTIFIER NULL,
    [driverId] UNIQUEIDENTIFIER NOT NULL, 
    [comment] NVARCHAR(4000) NOT NULL, 
    [dateTime] DATETIMEOFFSET NOT NULL, 
    CONSTRAINT [FK_LogEventAnnotation_LogEvent] FOREIGN KEY ([logEventId]) REFERENCES [LogEvent]([Id]) 
)
