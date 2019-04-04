CREATE TABLE [dbo].[DriverBreakRule] (
    [Id]         UNIQUEIDENTIFIER   CONSTRAINT [DF_DriverBreakRules_Id] DEFAULT (newid()) NOT NULL,
    [driverId]   UNIQUEIDENTIFIER   NOT NULL,
    [activeFrom] DATETIMEOFFSET (7) NOT NULL,
    [activeTo]   DATETIMEOFFSET (7) NOT NULL,
    [country]    NVARCHAR (50)      NULL,
    [region]     NVARCHAR (50)      NULL,
    CONSTRAINT [PK_DriverBreakRules] PRIMARY KEY CLUSTERED ([Id] ASC)
);

