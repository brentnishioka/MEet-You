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

-----------------------------

-- Testing Creating Itinerary
EXEC MEetAndYou.CreateItinerary @itineraryName = 'Monster Hunter Collections', @itineraryRating = 3, @itineraryOwner = 3;

-- Testing etting all Itineraries
EXEC MEetAndYou.GetItinerary;

-- Testing getting itineraries by itinID
EXEC MEetAndYou.GetItineraryByID @itineraryID = 5;

--TTesting getting itineraries by ownerID
EXEC MEetAndYou.GetItineraryByOwnerID @ownerID = 3;

-- Testing update itineraryName
EXEC MEetAndYou.UpdateItineraryName @itineraryID = 10, @itineraryName = 'Demon Slayer Adventure';

-- Testing update itinerary rating
EXEC MEetAndYou.UpdateItineraryRating @itineraryID = 10, @itineraryRating = 1;

-- Testing removing an itinerary by itineraryID
EXEC MEetAndYou.DeleteItineraryByID @itineraryID = 10;

--------------------------------

-- Testing creating a new UserItinerary
EXEC MEetAndYou.CreateUserItinerary @userID = 3, @itineraryID = 9;
EXEC MEetAndYou.CreateUserItinerary @userID = 8, @itineraryID = 9;

-- Testing get all UserItinerary
EXEC MEetAndYou.GetUserItinerary;

-- Testing get UserITinerary by userID
EXEC MEetAndYou.GetUserItineraryByUserID @userID = 3;

-- Testing get all users associated with an itinerary
EXEC MEetAndYou.GetUsersByItineraryID @itineraryID = 5;

-- Testing delete UserItinerary by itineraryID and userID
EXEC MEetAndYou.DeleteUserItinerary @itineraryID = 9, @userID = 3;

-- Testing delete UserItinerary by itineraryID only
EXEC MEetAndYou.DeleteUserItineraryByItinID @itineraryID = 9;