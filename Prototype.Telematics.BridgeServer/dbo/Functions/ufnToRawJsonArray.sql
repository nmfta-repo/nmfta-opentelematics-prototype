-- =============================================
-- Author:		Joanne Popper
-- Create date: 5/14/2019
-- Description:	given an json array of objects with the given key, return just an array of values
-- Example: SELECT dbo.ufnToRawJsonArray('Factor', '[{"Factor":"upstream server operational"},{"Factor":"authentication service operational"}]')
--			will return ["upstream server operational","authentication service operational"]
-- =============================================
CREATE FUNCTION [dbo].[ufnToRawJsonArray] 
(
	-- Add the parameters for the function here
	@objectKey varchar(50),
	@jsonArray varchar(max)
)
RETURNS varchar(max)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result varchar(max)
	SET @objectKey = '"' + @objectKey + '":'

	SET @jsonArray =  REPLACE(@jsonArray, '{', '')
	SET @jsonArray =  REPLACE(@jsonArray, '}', '')
	SET @jsonArray =  REPLACE(@jsonArray, @objectKey, '')


	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = @jsonArray

	-- Return the result of the function
	RETURN @Result

END
GO


