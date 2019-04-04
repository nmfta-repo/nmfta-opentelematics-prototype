CREATE TABLE [dbo].[DriverWaiver] (
    [Id]        UNIQUEIDENTIFIER   CONSTRAINT [DF_DriverWaivers_Id] DEFAULT (newid()) NOT NULL,
    [driverId]  UNIQUEIDENTIFIER   NOT NULL,
    [country]   NVARCHAR (50)      NOT NULL,
    [region]    NVARCHAR (50)      NOT NULL,
    [waiverDay] DATETIMEOFFSET (7) NOT NULL,
    CONSTRAINT [PK_DriverWaivers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

