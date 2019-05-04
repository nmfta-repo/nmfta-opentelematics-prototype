CREATE TABLE [dbo].[Driver] (
    [Id]            UNIQUEIDENTIFIER CONSTRAINT [DF_Driver_Id] DEFAULT (newid()) NOT NULL,
    [username]            NVARCHAR (250)   NOT NULL,
    [driverLicenseNumber] NVARCHAR (50)    NOT NULL,
    [country]             NVARCHAR (50)    NOT NULL,
    [region]              NVARCHAR (50)    NOT NULL,
    [driverHomeTerminal]  NVARCHAR(50)       NULL,
    [password] NVARCHAR(250) NULL, 
    [enabled] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_Driver] PRIMARY KEY CLUSTERED ([Id] ASC)
);

