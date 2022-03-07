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
(2, 'User'),
(3, 'User'),
(4, 'User'),
(5, 'User'),
(7, 'User'),
(8, 'User');

INSERT INTO MEetAndYou.UserRole (UserID, role) values 
(1, 'User');

DROP TABLE MEetAndYou.UserRole;
DROP TABLE MEetAndYou.UserToken;

DROP TABLE MEetAndYou.Event;
DROP TABLE MEetAndYou.Category;

SELECT * from MEetAndYou.UserAccountRecords;

  CREATE TABLE [MEetAndYou].[Category] (
		categoryName varchar (50) not null,

		constraint category_pk primary key (categoryName)
  );

  CREATE TABLE [MEetAndYou].[Event] (
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

		constraint eventID_fk FOREIGN KEY (eventID) references MEetAndYou.Event (eventID),
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

  SELECT * from MEetAndYou.UserItinerary;