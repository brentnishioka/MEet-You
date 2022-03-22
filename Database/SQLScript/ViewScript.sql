
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

