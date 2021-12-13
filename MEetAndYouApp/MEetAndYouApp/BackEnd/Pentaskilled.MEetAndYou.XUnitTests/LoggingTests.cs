using Xunit;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

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

            bool isSuccessfullyCreated = true;
            Assert.Equal(isSuccessfullyCreated, systemLoggingService.CreateNewLog(DateTime.UtcNow, "View", LogLevel.Debug, "This is a test debug log."));
        }

        [Fact]
        public void CreateUserLogUsingServiceTest()
        {
            IUserLoggingService userLoggingService = new UserLoggingService();

            bool isSuccessfullyCreated = true;
            Assert.Equal(isSuccessfullyCreated, userLoggingService.CreateNewLog(DateTime.UtcNow, "Data Access", LogLevel.Debug, 592, "This is a test debug log."));
        }

        [Fact]
        public void CreateSysLogFromLogManagerTest()
        {
            LoggingManager logMan = new LoggingManager();

            string category = "Data";
            LogLevel level = LogLevel.Error;
            string message = "Data not processed correctly.";

            bool isSuccessfullyCreated = true;
            bool actualLogProcessResult = logMan.BeginLogProcess(category, level, message);

            Assert.Equal(isSuccessfullyCreated, actualLogProcessResult);
        }

        [Fact]
        public void CreateUserLogFromLogManagerTest()
        {
            LoggingManager logMan = new LoggingManager();

            string category = "Data";
            LogLevel level = LogLevel.Error;
            int userId = 69;
            string message = "User Data not processed correctly.";

            bool isSuccessfullyCreated = true;
            bool actualLogProcessResult = logMan.BeginLogProcess(category, level, userId, message);

            Assert.Equal(isSuccessfullyCreated, actualLogProcessResult);
        }

        [Fact]
        public void SysLoggingFailCase1()
        {
            var reqStartTime = DateTime.UtcNow;

            LoggingManager logMan = new LoggingManager();

            string category = "Business";
            LogLevel level = LogLevel.Error;
            string message = "Business rule not implemented correctly.";

            logMan.BeginLogProcess(category, level, message);

            var reqEndTime = DateTime.UtcNow;
            var timeDifference = reqEndTime - reqStartTime;

            Assert.True(timeDifference.TotalSeconds < 5.0);
        }

        [Fact]
        public void UserLoggingFailCase1()
        {
            var reqStartTime = DateTime.UtcNow;

            LoggingManager logMan = new LoggingManager();

            string category = "Data Store";
            LogLevel level = LogLevel.Error;
            int userId = 69;
            string message = "User account record not inserted into the database successfully.";

            logMan.BeginLogProcess(category, level, userId, message);

            var reqEndTime = DateTime.UtcNow;
            var timeDifference = reqEndTime - reqStartTime;

            Assert.True(timeDifference.TotalSeconds < 5.0);
        }

        [Fact]
        public async void SysLoggingFailCase2()
        {
            LoggingManager logMan = new LoggingManager();

            string category = "Business";
            LogLevel level = LogLevel.Error;
            string message = "This is a message to indicate an error in the business layer.";

            Task<bool> sysTask = Task.Run(() =>
                logMan.BeginLogProcess(category, level, message)
            );
            bool result = await sysTask;

            Assert.True(result);
        }

        [Fact]
        public async void UserLoggingFailCase2()
        {
            LoggingManager logMan = new LoggingManager();

            string category = "View";
            LogLevel level = LogLevel.Warning;
            int userId = 420;
            string message = "User unable to interact with the system.";

            Task<bool> sysTask = Task.Run(() =>
                logMan.BeginLogProcess(category, level, userId, message)
            );
            await sysTask;

            Assert.True(sysTask.IsCompleted);
        }

        [Fact]
        public void SysLoggingFailCase4()
        {
            ISystemLoggingService systemLoggingService = new SystemLoggingService();

            DateTime dateTime = DateTime.UtcNow;
            string category = "Server";
            LogLevel level = LogLevel.Error;
            string message = "Web server crashed.";

            Assert.True(systemLoggingService.CreateNewLog(dateTime, category, level, message));
        }

        [Fact]
        public void UserLoggingFailCase4()
        {
            IUserLoggingService userLoggingService = new UserLoggingService();

            DateTime dateTime = DateTime.UtcNow;
            string category = "View";
            LogLevel level = LogLevel.Warning;
            int userId = 49230;
            string message = "User unable to interact with the system.";

            Assert.True(userLoggingService.CreateNewLog(dateTime, category, level, userId, message));
        }


        [Fact]
        public void SysLoggingFailCase5()
        {
            ISystemLoggingService systemLoggingService = new SystemLoggingService();

            DateTime dateTime = DateTime.UtcNow;
            string category = "View";
            LogLevel level = LogLevel.Error;
            string message = "Error: system logs should not be modifiable";

            Assert.True(systemLoggingService.CreateNewLog(dateTime, category, level, message));
        }

        [Fact]
        public void UserLoggingFailCase5()
        {
            IUserLoggingService userLoggingService = new UserLoggingService();

            DateTime dateTime = DateTime.UtcNow;
            string category = "View";
            LogLevel level = LogLevel.Error;
            int userId = 49230;
            string message = "Error: user logs should not be modifiable";

            Assert.True(userLoggingService.CreateNewLog(dateTime, category, level, userId, message));
        }




    }
}