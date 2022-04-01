USE [MEetAndYou-DB]
GO
CREATE FUNCTION [MEetAndYou].GetRolesByID
( @UserID int
)
RETURNS TABLE
    RETURN ( select role from MEetAndYou.UserRole where UserID = @UserID )
;
GO

-------------------------------------------
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
    SELECT @salt = [Salt] FROM [MEetAndYou].[UserAccountRecords]
    WHERE [UserId] = @userID

    DECLARE @hashedToken nvarchar(64)
    SELECT @hashedToken = HASHBYTES('SHA2_512', @token+@salt)

    SET @uId = (SELECT [UserID] FROM [MEetAndYou].[UserToken]
                WHERE [token] = @hashedToken);

    -- Return the result of the function
    RETURN @uId
END
GO

select * from MEetAndYou.UserRole;
select * from MEetAndYou.GetRolesByID(2);