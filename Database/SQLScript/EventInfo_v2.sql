SELECT Event.eventID, Event.eventName, Event.description, Event.address, Event.price, Event.eventDate, EventCategory.categoryName
FROM MEetAndYou.Event, MEetAndYou.EventCategory
WHERE MEetAndYou.Event.eventID = 2;