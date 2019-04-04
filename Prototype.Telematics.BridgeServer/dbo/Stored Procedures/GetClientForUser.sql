-- =============================================
-- Description:	Get Client for Passed in User Id
-- =============================================
CREATE PROCEDURE GetClientForUser
(
	@UserId nvarchar(450)
) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ClientId, ClientName, Status From ClientMaster C
	Where C.ClientId = (Select TOP 1 ClientId From ClientUserXRef Where UserId = @UserId)
END
