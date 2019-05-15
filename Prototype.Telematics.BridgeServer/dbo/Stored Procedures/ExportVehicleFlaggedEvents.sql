-- =============================================
-- Author:		Joanne Popper
-- Create date: 05/14/2019
-- Description:	Export Vehicle data for a given date,  
--				in JSON format, per Open Telematics API specifications 
-- =============================================
CREATE PROCEDURE [dbo].[ExportVehicleFlaggedEvents] 
	-- Add the parameters for the stored procedure here
	@SELECT_DATE DATE

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @PROVIDER_ID NVARCHAR(20) 
	SET @PROVIDER_ID = 'otapidemo.nmfta.org'

	DECLARE @SERVER_TIME DATETIMEOFFSET(7)
	SET @SERVER_TIME = GETUTCDATE()

	SELECT 
		@PROVIDER_ID as providerId
		,@SERVER_TIME as serverTime
		,[Id]
		,[vehicleId]
		,[longitude]
		,[latitude]
		,[eventComment]
		,[triggerDate] as 'triggeredDate'
		,[clearedDate]
		,[occurences]
		,[messageIdentifier]
		,[parameterOrSubsystemIdType]
		,[faultCodeParameterOrSubsystemId]
		,[sourceAddress]
		,[suspectParameterNumber]
		,[failureModeIdentifier]
		,[urgentFlag]
		,[odometer]
		,[engineRpm]
		,[ecmSpeed]
		,[ccSwitch] as 'cruiseStatus.ccSwitch'
		,[ccSetSwitch] as 'cruiseStatus.ccSetSwitch'
		,[ccCoastSwitch] as 'cruiseStatus.ccCoastSwitch'
		,[ccClutchSwitch] as 'cruiseStatus.ccClutchSwitch'
		,[ccCruiseSwitch] as 'cruiseStatus.ccCruiseSwitch'
		,[ccResumeSwitch] as 'cruiseStatus.ccResumeSwitch'
		,[ccAccelerationSwitch] as 'cruiseStatus.ccAccelerationSwitch'
		,[ccBrakeSwitch] as 'cruiseStatus.ccBrakeSwitch'
		,[ccSpeed] as 'cruiseStatus.ccSpeed'
		,[ignitionAccessory] as 'ignitionStatus.ignitionAccessory'
		,[ignitionRunContact] as 'ignitionStatus.ignitionRunContact'
		,[ignitionCrankContact] as 'ignitionStatus.ignitionCrankContact'
		,[ignitionAidContact] as 'ignitionStatus.ignitionAidContact'
		,[gpsQuality]
		,[clearType]
	FROM [dbo].[VehicleFaultCodeEvent]
	WHERE CONVERT(date, [triggerDate]) = @SELECT_DATE
	AND [urgentFlag] = 1
	FOR JSON PATH 

END
GO


