-- Function to verify the user token in UserToken table and return count of ID
USE [MEetAndYou-DB]
GO
CREATE FUNCTION [MEetAndYou].[VerifyUserToken]
(
    @userID INT,
    @token VARCHAR(25)
)
RETURNS INT
AS
BEGIN
    -- Declare the return variable here
    DECLARE @uId INT;

    DECLARE @salt nvarchar(64)
    SELECT @salt = [Salt] FROM [MEetAndYou].[UserToken]
    WHERE [UserId] = @userID

    DECLARE @hashedToken nvarchar(64)
    SELECT @hashedToken = HASHBYTES('SHA2_512', @token+@salt)

	SELECT @uID =	COUNT([UserID]) FROM [MEetAndYou].[UserToken] 
    WHERE [token] = @hashedToken

    -- Return the result of the function
    RETURN @uId
END
GO