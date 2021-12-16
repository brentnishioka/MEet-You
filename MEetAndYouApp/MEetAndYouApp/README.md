# How to run Logging, User Management, and Archiving tests

1. Clone or download the MEet-You repository and open the MEetAndYouApp.sln file in the backend folder using visual studio.
2. Open Micorsoft SQL Server Managment Studio
- Connect to a new Database Engine.
- Set the serve name as localhost.
- Use Windows Authentication.
- Right click on the Databases folder and click Restore Database.
- Select device as the source and navigate to MEet-You/MEetAndYouApp/MEetAndYouApp/BackEnd/Database/MEetAndYouDB_Backup_20211215.bak within the repo.
- Upon locating the DB, hover over to options and uncheck Tail-Log Backup then press OK.
- Right click on the server and select new query. 
- Locate the contents of  MEet-You/MEetAndYouApp/MEetAndYouApp/BackEnd/Database/LoginConfig.sql and paste the code into the query editor and execute.
3. Navigate back to Visual Studio, select tests ---> Run All Tests.

