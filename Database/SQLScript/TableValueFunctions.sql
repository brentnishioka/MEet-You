USE [MEetAndYou-DB]
GO
CREATE FUNCTION [MEetAndYou].GetRolesByID
( @UserID int
)
RETURNS TABLE
    RETURN ( select role from MEetAndYou.UserRole where UserID = @UserID )
;
GO

select * from MEetAndYou.UserRole;
select * from MEetAndYou.GetRolesByID(2);