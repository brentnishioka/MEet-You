
USE [MEetAndYou-DB]
GO
/****** Object:  UserDefinedFunction [MEetAndYou].[GetEventCategory]    Script Date: 3/6/2022 9:06:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Return the category of a specific event

CREATE FUNCTION [MEetAndYou].[GetEventCategory] (@eventID int) 
RETURNS TABLE 
AS 
RETURN 
(
	SELECT * FROM MEetAndYou.EventCategory 
	WHERE EventCategory.eventID = @eventID
)
