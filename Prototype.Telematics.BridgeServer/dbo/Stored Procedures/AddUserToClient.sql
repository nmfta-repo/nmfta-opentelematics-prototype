
-- =============================================
-- Description:	Add User To Client
-- =============================================
CREATE PROCEDURE AddUserToClient
(
	@ClientId nvarchar(450),
	@UserId nvarchar(450)
) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (NOT EXISTS (SELECT UserId FROM ClientUserXRef WHERE ClientId=@ClientId AND UserId=@UserId))
	BEGIN
		INSERT INTO ClientUserXRef(ClientId,UserId)
		VALUES (@ClientId,@UserId)
	END
END
