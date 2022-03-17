CREATE TABLE MEetAndYou.UserSession (
    UserID int NOT NULL, 
    userHash binary NOT NULL,
    sessionDate DateTime NOT NULL, 
    
    constraint userSession_pk PRIMARY KEY (UserID, userHash)

);
   
    
