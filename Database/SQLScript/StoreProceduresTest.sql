SELECT * from MEetAndYou.Category;
SELECT * from MEetAndYou.Events;

EXEC MEetAndYou.AddCategory @categoryName = 'boba';

EXEC MEetAndYou.RemoveCategory @categoryName = 'boba';

EXEC MEetAndYou.UpdateCategory @oldcategoryName = 'boba', @newcategoryname = 'LAN party';

EXEC MEetAndYou.AddEvent @eventName = 'Fortnite Gamenight', @eventDescription = 'Lets get some winner winner chicken dinner', @eventAddress = '234 Epic st, Anaheim', @price = 0, @eventDate = '2022-03-24';

DELETE FROM MEetAndYou.Events where eventName = 'Fortnite Gamenight';