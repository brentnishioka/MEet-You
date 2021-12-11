using Xunit;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Logging;
using System;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class LoggingTests
    {
        [Fact]
        public void InsertSysLogIntoDBTest()
        {
            SystemLog sysLog = new SystemLog();
            sysLog.dateTime = DateTime.UtcNow;
            sysLog.category = "Server";
            sysLog.logLevel = LogLevel.Error;
            sysLog.message = "Web server crashed.";

            ILogDAO logDAO = new LogDAO();
            bool isSuccessfullyInserted = true;

            Assert.Equal(isSuccessfullyInserted, logDAO.PushLogToDB(sysLog));
        }

        [Fact]
        public void InsertUserLogIntoDBTest()
        {
            UserLog userLog = new UserLog();
            userLog.dateTime = DateTime.UtcNow;
            userLog.category = "View";
            userLog.logLevel = LogLevel.Info;
            userLog.userId = 100;
            userLog.message = "ERROR 252";

            ILogDAO logDAO = new LogDAO();
            bool isSuccessfullyInserted = true;

            Assert.Equal(isSuccessfullyInserted, logDAO.PushLogToDB(userLog));
        }

        [Fact]
        public void CreateSystemLogUsingServiceTest()
        {
            ISystemLoggingService systemLoggingService = new SystemLoggingService();
            systemLoggingService.CreateNewLog(DateTime.UtcNow, "View", LogLevel.Debug, "This is a test debug log.");
        }

        [Fact]
        public void CreateUserLogUsingServiceTest()
        {
            IUserLoggingService userLoggingService = new UserLoggingService();
            userLoggingService.CreateNewLog(DateTime.UtcNow, "Data Access", LogLevel.Debug, 592, "This is a test debug log.");
        }
    }
}