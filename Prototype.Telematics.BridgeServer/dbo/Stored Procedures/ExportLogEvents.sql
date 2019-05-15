-- =============================================
-- Author:		Joanne Popper
-- Create date: 05/15/2019
-- Description:	Export Log Event data for a given date,  
--				in JSON format, per Open Telematics API specifications 
-- =============================================
CREATE PROCEDURE [dbo].[ExportLogEvents] 
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
		,[dateTime]
		,[vehicleId]
		,[driverId]
		,[coDrivers]
		,[distanceLastValid]
		,[editDateTime]
		,[locationId]
		,[origin]
		,[parentId]
		,[sequence]
		,[state]
		,[eventType]
		,[certificationCount]
		,[verifyDateTime]
		,[multidayBasis]
		,[comment]
		,[eventDataChecksum]
	FROM LogEvent
	WHERE CONVERT(date, [dateTime]) = @SELECT_DATE

	FOR JSON AUTO

END
GO


