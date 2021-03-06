-- =============================================
-- Author:		Joanne Popper
-- Create date: 05/14/2019
-- Description:	Export Vehicle data for a given date,  
--				in JSON format, per Open Telematics API specifications 
-- =============================================
CREATE PROCEDURE [dbo].[ExportVehicles] 
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
	,[name]
	,[cmvVIN]
	,[licensePlate]
	FROM Vehicle
	FOR JSON AUTO

END
GO


