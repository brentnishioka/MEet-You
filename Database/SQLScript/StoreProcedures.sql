select * from MEetAndYou.Events
select * from MEetAndYou.Category

-- Store Procedures for Category table
-- Add procedure for Adding category
USE [MEetAndYou-DB]
GO
CREATE PROCEDURE  MEetAndYou.CreateCategory
    @categoryName varchar(50)    
AS   
	INSERT INTO MEetAndYou.Category (categoryName) values
		(@categoryName)

GO

-- Add procedure for removing 
GO
CREATE PROCEDURE MEetAndYou.DeleteCategory
    @categoryName varchar(50)    
AS   
	DELETE FROM MEetAndYou.Category where categoryName = @categoryName

GO

-- Procedure for updating a category name
GO
CREATE PROCEDURE MEetAndYou.UpdateCategory
    @oldcategoryName varchar(50),
	@newcategoryName varchar(50) 
AS   
	UPDATE MEetAndYou.Category 
	SET categoryName = @newcategoryName
	where categoryName = @oldcategoryName

GO

-- Store Procedures for Events Table
-- Procedure for adding a new event to the database
USE [MEetAndYou-DB]
GO
CREATE PROCEDURE  MEetAndYou.CreateEvent
    @eventName varchar(35), 
	@eventDescription varchar(350),
	@eventAddress varchar (50), 
	@price float, 
	@eventDate DATETIME

AS   
	INSERT INTO MEetAndYou.Events (eventName, description, address, price, eventDate) values
		(@eventName, @eventDescription, @eventAddress, @price, @eventDate)

GO

USE [MEetAndYou-DB]
-- Procedure for Updating an existing event
--Updateing an event name knowing the eventID
GO
CREATE PROCEDURE  MEetAndYou.UpdateEventName
	@eventID int,
    @neweventName varchar(35)

AS   
	UPDATE MEetAndYou.Events
	SET eventName = @neweventName
	where eventID = @eventID
GO

-- Procedure for updating event description using the eventID
GO
CREATE PROCEDURE  MEetAndYou.UpdateEventDescription
	@eventID int,
    @eventDescription varchar(350)

AS   
	UPDATE MEetAndYou.Events
	SET description = @eventDescription
	where eventID = @eventID
GO

-- Procedure for updating event address using the eventID
GO
CREATE PROCEDURE  MEetAndYou.UpdateEventAddress
	@eventID int,
    @eventAddress varchar(50)

AS   
	UPDATE MEetAndYou.Events
	SET address = @eventAddress
	where eventID = @eventID
GO

-- Procedure for updating event price using the eventID
GO
CREATE PROCEDURE  MEetAndYou.UpdateEventPrice
	@eventID int,
    @eventPrice float

AS   
	UPDATE MEetAndYou.Events
	SET price = @eventPrice
	where eventID = @eventID
GO

-- Procedure for updating event date using the eventID
USE [MEetAndYou-DB]
GO
CREATE PROCEDURE  MEetAndYou.UpdateEventDate
	@eventID int,
    @eventDate datetime

AS   
	UPDATE MEetAndYou.Events
	SET eventDate = @eventDate
	where eventID = @eventID
GO

-- Procedures for removing an Event from the Events table 
-- Remove an event using eventID
USE [MEetAndYou-DB]
GO
CREATE PROCEDURE MEetAndYou.DeleteEvent
    @eventID int   
AS   
	DELETE FROM MEetAndYou.Events where eventID = @eventID

GO

--Procedures for EventCategory Table
--Procedures for creating new eventCategory 

USE [MEetAndYou-DB]
GO
CREATE PROCEDURE  MEetAndYou.CreateEventCategory
    @eventID int, 
	@categoryName varchar(50)

AS   
	INSERT INTO MEetAndYou.EventCategory(eventID, categoryName) values
		(@eventID, @categoryName)

GO

--Procedure for Updating an EventCategory
GO
CREATE PROCEDURE  MEetAndYou.UpdateEventCategory
    @eventID int, 
	@categoryName varchar(50)

AS   
	UPDATE MEetAndYou.EventCategory
	SET categoryName = @categoryName
	where eventID = @eventID
GO

--Procedures for removing an EventCategory
USE [MEetAndYou-DB]
GO
CREATE PROCEDURE MEetAndYou.DeleteEventCategory
    @eventID int,
	@categoryName varchar(50)
AS   
	DELETE FROM MEetAndYou.EventCategory where eventID = @eventID and categoryName = @categoryName

GO