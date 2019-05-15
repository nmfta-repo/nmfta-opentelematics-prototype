-- =============================================
-- Author:		Joanne Popper
-- Create date: 05/14/2019
-- Description:	Export Performance Threshold data for a given date,  
--				in JSON format, per Open Telematics API specifications 
-- =============================================
CREATE PROCEDURE [dbo].[ExportPerformanceThresholds] 
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
		,[activeFrom]
		,[activeTo]
		,[rpmOverValue]
		,[overSpeedValue]
		,[excessSpeedValue]
		,[longIdleValue]
		,[hiThrottleValue]
	FROM [dbo].[VehiclePerformanceThreshold]
	FOR JSON AUTO 

END
GO


