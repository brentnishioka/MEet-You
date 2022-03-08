	SELECT *
	from MEetAndYou.Event 
	inner join 
	MEetAndYou.EventCategory
	--using (eventID)
	on Event.eventID = EventCategory.eventID
	WHERE MEetAndYou.Event.eventID = 2;

	SELECT *
	from MEetAndYou.Event 
	inner join 
	MEetAndYou.EventCategory
	using (eventID);
	--on Event.eventID = EventCategory.eventID
	--WHERE MEetAndYou.Event.eventID = 2;