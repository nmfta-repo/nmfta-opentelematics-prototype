CREATE TABLE [dbo].[ClientMaster] (
    [ClientId]   NVARCHAR (450) CONSTRAINT [DF_ClientMaster_ClientId] DEFAULT (newid()) NOT NULL,
    [ClientName] NVARCHAR (500) NOT NULL,
    [Status]     NVARCHAR (25)  CONSTRAINT [DF_ClientMaster_Status] DEFAULT (N'Active') NOT NULL,
    CONSTRAINT [PK_ClientMaster] PRIMARY KEY CLUSTERED ([ClientId] ASC)
);

