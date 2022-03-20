SELECT * from MEetAndYou.Category;

EXEC MEetAndYou.AddCategory @categoryName = 'boba';

EXEC MEetAndYou.RemoveCategory @categoryName = 'boba';

EXEC MEetAndYou.UpdateCategory @oldcategoryName = 'boba', @newcategoryname = 'LAN party';