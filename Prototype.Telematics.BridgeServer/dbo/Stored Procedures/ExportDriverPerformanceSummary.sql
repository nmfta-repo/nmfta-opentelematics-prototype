-- =============================================
-- Author:		Joanne Popper
-- Create date: 05/15/2019
-- Description:	Export Driver Performance Summary data for a given date,  
--				in JSON format, per Open Telematics API specifications 
-- =============================================
CREATE PROCEDURE [dbo].[ExportDriverPerformanceSummary] 
	@SELECT_DATE DATE
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @PROVIDER_ID NVARCHAR(20) 
	SET @PROVIDER_ID = 'otapidemo.nmfta.org'

	DECLARE @SERVER_TIME DATETIMEOFFSET(7)
	SET @SERVER_TIME = GETUTCDATE()

	SELECT 
		@PROVIDER_ID as providerId 
		,@SERVER_TIME as serverTime
		,[Id]
		,[driverId]
		,[eventStart]
		,[eventEnd]
		,[distance]
		,[fuel]
		,[cruiseTime]
		,[engineLoadPercent]
		,[overRpmTime]
		,[brakeEvents]
	FROM DriverPerformanceSummary
	WHERE CONVERT(date, [eventStart]) = @SELECT_DATE
	FOR JSON AUTO

END
GO


