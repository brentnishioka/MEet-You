
USE [MEetAndYou-DB]

-- Find all the events of available
GO
CREATE VIEW MEetAndYou.AllEvents 
AS
SELECT * from MEetAndYou.Events
GO

GO
CREATE VIEW MEetAndYou.AllCategories 
AS
SELECT * from MEetAndYou.Category
GO

Select * from MEetAndYou.Events;
SELECT * from MEetAndYou.Category
-- MEetAndYou.UpdateEventName @eventID = 13 , @neweventName = 'Fortnite Party night';
