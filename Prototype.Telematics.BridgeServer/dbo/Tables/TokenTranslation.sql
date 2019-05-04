CREATE TABLE [dbo].[TokenTranslation]
(
	[msgId] NVARCHAR(100) NOT NULL PRIMARY KEY, 
    [origin] NVARCHAR(500) NOT NULL, 
    [msgstr] NVARCHAR(1000) NOT NULL,
)
