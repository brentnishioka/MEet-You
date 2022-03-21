select * from MEetAndYou.Events
select * from MEetAndYou.Category

-- Store Procedures for Category table
-- Add procedure for Adding category
GO
CREATE PROCEDURE  MEetAndYou.AddCategory
    @categoryName varchar(50)    
AS   
	INSERT INTO MEetAndYou.Category (categoryName) values
		(@categoryName)

GO

-- Add procedure for removing 
GO
CREATE PROCEDURE MEetAndYou.RemoveCategory
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
GO
CREATE PROCEDURE  MEetAndYou.AddEvent
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
GO
CREATE PROCEDURE  MEetAndYou.UpdateEventDate
	@eventID int,
    @eventDate float

AS   
	UPDATE MEetAndYou.Events
	SET eventDate = @eventDate
	where eventID = @eventID
GO