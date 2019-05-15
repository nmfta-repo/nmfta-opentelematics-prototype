-- =============================================
-- Author:		Joanne Popper
-- Create date: 05/14/2019
-- Description:	Export Vehicle performance events for a given date,  
--				in JSON format, per Open Telematics API specifications 
-- =============================================
CREATE PROCEDURE [dbo].[ExportVehiclePerformanceEvents] 
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
		,VPE.[Id]
		,[eventStart]
		,[eventEnd]
		,[vehicleId]
		,[eventComment]
		,[hours]
		--,[performanceThresholdId]
		,[odometerStart]
		,[odometerEnd]
		,[engineTime]
		,[movingTime]
		,[startFuel]
		,[endFuel]
		,[brakeApplications]
		,[engineLoadStopped]
		,[engineLoadMoving]
		,[headlightTime]
		,[speedGovernorValue]
		,[overRpmTime]
		,[overSpeedTime]
		,[excessSpeedTime]
		,[longIdleTime]
		,[shortIdleTime]
		,[shortIdleCount]
		,[longIdleFuel]
		,[shortIdleFuel]
		,[cruiseEvents]
		,[cruiseTime]
		,[cruiseFuel]
		,[cruiseDistance]
		,[topGearValue]
		,[topGearTime]
		,[topGearFuel]
		,[topGearDistance]
		,[ptoFuel]
		,[ptoTime]
		,[seatBeltTime]
		,[particulateFilterStatus]
		,[exhaustFluidLevel]
		,[overspeedLowThrottle]
		,[overspeedHiThrottle]
		,[overrpmLowThrottle]
		,[overrpmHiThrottle]
		,[lkaDisable]
		,[ldwActive]
		,[ldwDisable]
		,[lkaActive]
		,VPT.[Id] AS 'thresholds.id'
		,@PROVIDER_ID as 'thresholds.providerId'
		,@SERVER_TIME as 'thresholds.serverTime'
		,VPT.[activeFrom] AS 'thresholds.activeFrom'
		,VPT.[activeTo] AS 'thresholds.activeTo'
		,VPT.[rpmOverValue] AS 'thresholds.rpmOverValue'
		,VPT.[overSpeedValue] AS 'thresholds.overSpeedValue' 
		,VPT.[excessSpeedValue] AS 'thresholds.excessSpeedValue' 
		,VPT.[longIdleValue] AS 'thresholds.longIdleValue' 
		,VPT.[hiThrottleValue] AS 'thresholds.hiThrottleValue' 
	FROM [dbo].[VehiclePerformanceEvent] VPE
	INNER JOIN [dbo].[VehiclePerformanceThreshold] VPT
	ON VPE.performanceThresholdId = VPT.Id
	WHERE CONVERT(date, [eventStart]) = @SELECT_DATE
	FOR JSON PATH 

END
GO


