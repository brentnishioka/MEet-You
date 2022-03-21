SELECT * from MEetAndYou.Category;
SELECT * from MEetAndYou.Events;
SELECT * from MEetAndYou.EventCategory;

EXEC MEetAndYou.AddCategory @categoryName = 'boba';

EXEC MEetAndYou.RemoveCategory @categoryName = 'boba';

EXEC MEetAndYou.UpdateCategory @oldcategoryName = 'boba', @newcategoryname = 'LAN party';

-- Testing store procedures for creating an event

EXEC MEetAndYou.CreateEvent @eventName = 'Fortnite Gamenight', @eventDescription = 'Lets get some winner winner chicken dinner', @eventAddress = '234 Epic st, Anaheim', @price = 0, @eventDate = '2022-03-24';

DELETE FROM MEetAndYou.Events where eventName = 'Fortnite Gamenight';


-- Testing store procedures for Updating an event
-- Updating an event name
EXEC MEetAndYou.UpdateEventName @eventID = 13 , @neweventName = 'Fortnite Party night';

-- Updating a event description
EXEC MEetAndYou.UpdateEventDescription @eventID = 13 , @eventDescription = 'Lets 360 no scope some people';

-- Updating an event address
EXEC MEetAndYou.UpdateEventaddress @eventID = 13 , @eventAddress = '492 Mountain st, Anaheim';

-- Updating an event price
EXEC MEetAndYou.UpdateEventPrice @eventID = 13 , @eventPrice = 2.50;

-- Updating an event date
EXEC MEetAndYou.UpdateEventDate @eventID = 13 , @eventDate = '2022-03-20';


-- Testing store Procedures for Event Category

-- Testing CreateEventCategory procedure
EXEC MEetAndYou.CreateEventCategory @eventID = 13, @categoryName = 'Networking';

-- Updating the Category Name for EventCategory
EXEC MEetAndYou.UpdateEventCategory @eventID = 13, @categoryName = 'Sports';

-- Removing an EventCategory
EXEC MEetAndYou.DeleteEventCategory @eventID = 13, @categoryName = 'Sports';