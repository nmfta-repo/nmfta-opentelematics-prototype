﻿CREATE TABLE [dbo].[DriverPerformanceSummary] (
    [Id]                UNIQUEIDENTIFIER   CONSTRAINT [DF_DriverPerformanceSummary_Id] DEFAULT (newid()) NOT NULL,
    [driverId]          UNIQUEIDENTIFIER   NOT NULL,
    [eventStart]        DATETIMEOFFSET (7) NOT NULL,
    [eventEnd]          DATETIMEOFFSET (7) NOT NULL,
    [distance]          NUMERIC (18, 5)       NULL,
    [fuel]              NUMERIC (18, 5)       NULL,
    [cruiseTime]        NUMERIC (18, 5)       NULL,
    [engineLoadPercent] NUMERIC (18, 5)       NULL,
    [overRpmTime]       NUMERIC (18, 5)       NULL,
    [brakeEvents]       INT                NULL,
    CONSTRAINT [PK_DriverPerformanceSummary] PRIMARY KEY CLUSTERED ([Id] ASC)
);

