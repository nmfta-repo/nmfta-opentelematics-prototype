CREATE TABLE [dbo].[ClientUserXRef] (
    [ClientUserXRefId] UNIQUEIDENTIFIER CONSTRAINT [DF_ClientUserXRef_ClientUserXRefId] DEFAULT (newid()) NOT NULL,
    [ClientId]         NVARCHAR (450)   NOT NULL,
    [UserId]           NVARCHAR (450)   NOT NULL, 
    CONSTRAINT [PK_ClientUserXRef] PRIMARY KEY ([ClientUserXRefId])
);

