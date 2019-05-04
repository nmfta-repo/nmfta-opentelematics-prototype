﻿CREATE TABLE [dbo].[VehicleFaultCodeEvent]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [vehicleId] UNIQUEIDENTIFIER NOT NULL, 
    [longitude] NUMERIC(18, 8) NOT NULL, 
    [latitude] NUMERIC(18, 8) NOT NULL, 
    [eventComment] NVARCHAR(500) NULL, 
    [triggerDate] DATETIMEOFFSET NOT NULL, 
    [clearedDate] DATETIMEOFFSET NOT NULL, 
    [occurences] INT NOT NULL, 
    [messageIdentifier] INT NOT NULL, 
    [parameterOrSubsystemIdType] NVARCHAR(50) NOT NULL, 
    [faultCodeParameterOrSubsystemId] INT NOT NULL, 
    [sourceAddress] INT NOT NULL, 
    [suspectParameterNumber] INT NOT NULL, 
    [failureModeIdentifier] INT NOT NULL, 
    [urgentFlag] BIT NOT NULL, 
    [odometer] INT NOT NULL, 
    [engineRpm] INT NOT NULL, 
    [ecmSpeed] INT NOT NULL, 
    [ccSwitch] BIT NOT NULL, 
    [ccSetSwitch] BIT NOT NULL, 
    [ccCoastSwitch] BIT NOT NULL, 
    [ccClutchSwitch] BIT NOT NULL, 
    [ccCruiseSwitch] BIT NOT NULL, 
    [ccResumeSwitch] BIT NOT NULL, 
    [ccAccelerationSwitch] BIT NOT NULL, 
    [ccBrakeSwitch] BIT NOT NULL, 
    [ccSpeed] INT NOT NULL, 
    [ignitionAccessory] BIT NOT NULL, 
    [ignitionRunContact] BIT NOT NULL, 
    [ignitionCrankContact] BIT NOT NULL, 
    [ignitionAidContact] BIT NOT NULL, 
    [gpsQuality] NVARCHAR(50) NOT NULL, 
    [clearType] NVARCHAR(50) NULL, 
    CONSTRAINT [FK_VehicleFaultCodeEvent_Vehicle] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicle]([Id])
)
