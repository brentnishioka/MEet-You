
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

MEetAndYou.GetEvent @eventID = 5;

MEetAndYou.UpdateEventName @eventID = 13 , @neweventName = 'Fortnite Party night';