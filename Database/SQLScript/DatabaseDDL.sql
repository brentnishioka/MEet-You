USE [MEetAndYou-DB]
GO

/****** Object:  Table [MEetAndYou].[UserAccountRecords]    Script Date: 3/5/2022 4:35:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [MEetAndYou].[UserAccountRecords](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserEmail] [varchar](30) NULL,
	[UserPassword] [binary](64) NOT NULL,
	[UserPhoneNum] [varchar](15) NOT NULL,
	[UserRegisterDate] [varchar](30) NOT NULL,
	[Active] [bit] NOT NULL,
	[Salt] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [MEetAndYou-DB]
GO

CREATE TABLE [MEetAndYou].[Roles](
	[role] [varchar] (50) NOT NULL,
	PRIMARY KEY (role)
);

CREATE TABLE MEetAndYou.UserRole(
	UserID int NOT NULL,
	[role] [varchar] (50),
	FOREIGN KEY (UserID) references MEetAndYou.UserAccountRecords(userID),
	FOREIGN KEY (role) references MEetAndYou.Roles(role),
	constraint user_rolePK PRIMARY KEY (UserID, role)
);

CREATE TABLE MEetAndYou.UserToken(
	UserID int NOT NULL,
	token binary(64) NOT NULL,
	salt uniqueidentifier NULL,
	dateCreated varchar (30) NOT NULL,
	constraint userID_fk FOREIGN KEY (UserID) references MEetAndYou.UserAccountRecords(userID),
	constraint userRole_PK PRIMARY KEY (UserID, token)
);

INSERT INTO MEetAndYou.Roles values
('Admin'),
('User');

INSERT INTO MEetAndYou.UserRole (UserID, role) values 
(2, 'Admin'),
(2, 'User'),
(3, 'User'),
(4, 'User'),
(5, 'User'),
(7, 'User'),
(8, 'User');

INSERT INTO MEetAndYou.UserRole (UserID, role) values 
(1, 'User');
INSERT INTO MEetAndYou.UserRole (UserID, role) values 
(7, 'Admin');

DROP TABLE MEetAndYou.UserRole;
DROP TABLE MEetAndYou.UserToken;

DROP TABLE MEetAndYou.Event;
DROP TABLE MEetAndYou.Category;

SELECT * from MEetAndYou.UserAccountRecords;

  CREATE TABLE [MEetAndYou].[Category] (
		categoryName varchar (50) not null,

		constraint category_pk primary key (categoryName)
  );

  CREATE TABLE [MEetAndYou].[Events] (
		eventID int IDENTITY(1,1), 
		eventName varchar (35),
		description varchar (350),
		address varchar (50),
		price float, 
		eventDate DateTime

		constraint event_pk PRIMARY KEY (eventID)

  );

  CREATE TABLE MEetAndYou.EventCategory (
		eventID int, 
		categoryName varchar (50) not null

		constraint eventID_fk FOREIGN KEY (eventID) references MEetAndYou.Events (eventID),
		constraint category_fk FOREIGN KEY (categoryName) references MEetAndYou.Category (categoryName),
		constraint eventCategory_pk PRIMARY KEY (eventID, categoryName)
  );

  Drop table MEetAndYou.Itinerary;

  CREATE TABLE [MEetAndYou].[Itinerary] (
		itineraryID int IDENTITY(1,1), 
		itineraryName varchar (35) not null,
		rating int not null, 
		itineraryOwner int not null, 

		constraint itinOwner_fk FOREIGN KEY (itineraryOwner) references MEetAndYou.UserAccountRecords (UserID), 
		constraint itinerary_PK PRIMARY KEY (itineraryID)
  );

  CREATE TABLE [MEetAndYou].[UserItinerary] (
		itineraryID int not null, 
		UserID int not null, 

		constraint itineraryID_fk FOREIGN KEY (itineraryID) references MEetAndYou.Itinerary (itineraryID),
		constraint userIDitinerary_fk FOREIGN KEY (UserID) references MEetAndYou.UserAccountRecords (UserID), 
		constraint userItinerary_pk PRIMARY KEY (itineraryID, UserID)
  );

  -- Table for MemoryAlbum
  CREATE TABLE [MEetAndYou].[Images] (
		imageID int IDENTITY(1,1),
		imageName varchar(50) not null, 
		imageExtension varchar(50) not null, 
		imagePath varchar(200) not null, 
		itineraryID int not null

		constraint imageID_pk PRIMARY KEY (imageID), 
		constraint imageItineraryID_fk FOREIGN KEY (itineraryID) references MEetAndYou.Itinerary (itineraryID)
  ); 


  SELECT * from MEetAndYou.UserItinerary;
  SELECT * from MEetAndYou.Events
  SELECT * from MEetAndYou.EventCategory;
  SELECT * from MEetAndYou.Category;
  SELECT * from MEetAndYou.Roles;
  
  -- Dummy Data for Database 

  -- Data for Roles table
    INSERT INTO MEetAndYou.Roles(categoryName) values 
	('User'), 
	('Admin');

  -- Dummy Data for Category Table
  INSERT INTO MEetAndYou.Category (categoryName) values 
	('Sports'), 
	('Conferences'),
	('Expo'),
	('Concert'),
	('Festivals'), 
	('Performing Arts'), 
	('Workshop'), 
	('Food and Drink'),
	('Networking');

	DELETE FROM MEetAndYou.Events WHERE eventID = 3;
  -- Dummy Data for Events Table
  INSERT INTO MEetAndYou.Events (eventName, description, address, price, eventDate) values 
    ('KBBQ', 'eat the sadness away', '123 Garden Grove blvd, Garden Grove', 5, '2022-01-30'),
	('Canes chicken', 'not the best of chicken', '123 Cirle st, Long Beach', 8, '2022-02-18'),
	('Wings Stop', 'abomination of fried chicken', '234 Second st, Long Beach', 5, '2022-03-21'),
	('In and Out Burgers', 'bank for the buck', '123 Third st, Long Beach', 5, '2022-03-15'),
	('Popeyes Fried Chicken', 'decent for what the price is', '123 Valley st, Long Beach', 7.50, '2022-03-25'),
	('Rainbow Six Gamenight', 'Ash main goes brrrrrrrrt', '123 Chalet st, Long Beach', 0, '2022-03-18'),
	('League of Legends night', 'Burst damage goes brrrrrt', '123 Riot st, Long Beach', 0, '2022-03-19'),
	('Consolation boba', 'drink the sadness away', '123 Main st, Long Beach', 5, '2022-02-05');

  -- Dummy Data for EventCatogy Table
    INSERT INTO MEetAndYou.EventCategory (eventID, categoryName) values 
	( 4, 'Food and Drink'),
	( 5, 'Food and Drink'),
	( 6, 'Food and Drink'),
	( 7, 'Food and Drink'),
	( 8, 'Food and Drink'),
	( 9, 'Networking'),
	( 10, 'Networking'),
	( 11, 'Food and Drink');

	-- Dummy Data for Itinerary
	INSERT INTO MEetAndYou.Itinerary (itineraryName, rating, itineraryOwner) values 
	('Conan Cruise Itinierary', 5 , 3), 
	('Seven World Wonders', 5, 4),
	('World Food Tour', 3, 5), 
	('Kindaichi Adventure', 4, 7),
	('Lunastra Treat', 2, 8);

	-- Dumyy data for UserItinerary table
	INSERT INTO MEetAndYou.UserItinerary (itineraryID, userID) values 
	(5, 3),
	(6, 4),
	(7, 5),
	(8, 7),
	(9, 8),
	(5, 4),
	(6, 7),
	(6, 8);