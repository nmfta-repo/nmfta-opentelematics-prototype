-- =============================================
-- Author:		Joanne Popper
-- Create date: 05/14/2019
-- Description:	Export stopGeographicDetails data for a given date,  
--				in JSON format, per Open Telematics API specifications 
-- =============================================

CREATE PROCEDURE [dbo].[ExportStopGeographicDetails] 
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
		,[routeId]
		,[stopName]
		,[address]
		,[comment]
		,Cast([latitude] AS VARCHAR)  + ' ' + Cast([longitude] AS VARCHAR) as 'location'
		,[stopDeadline]
		,[entryArea]
	FROM StopGeographicDetails
	FOR JSON AUTO 

END
GO


