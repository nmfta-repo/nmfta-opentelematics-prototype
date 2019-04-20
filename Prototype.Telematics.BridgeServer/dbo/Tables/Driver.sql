CREATE TABLE [dbo].[Driver] (
    [Id]            UNIQUEIDENTIFIER CONSTRAINT [DF_Driver_Id] DEFAULT (newid()) NOT NULL,
    [username]            NVARCHAR (250)   NULL,
    [driverLicenseNumber] NVARCHAR (50)    NULL,
    [country]             NVARCHAR (50)    NULL,
    [region]              NVARCHAR (50)    NULL,
    [driverHomeTerminal]  NCHAR (10)       NULL,
    [password] NVARCHAR(250) NULL, 
    [enabled] BIT NULL DEFAULT 1, 
    CONSTRAINT [PK_Driver] PRIMARY KEY CLUSTERED ([Id] ASC)
);

