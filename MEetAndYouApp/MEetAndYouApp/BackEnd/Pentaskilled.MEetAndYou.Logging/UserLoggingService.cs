using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.Logging
{
    public class UserLoggingService : IUserLoggingService
    {
        private ILogDAO _logDataAccess;
        private UserLog _userLog;

        public UserLoggingService()
        {
            _logDataAccess = new LogDAO();
            _userLog = new UserLog();
        }

        public bool CreateNewLog(DateTime dateTime, string category, LogLevel logLevel, int userId, string message)
        {
            try
            {
                MakeLog(dateTime, category, logLevel, userId, message);
                int existLogCount = _logDataAccess.CheckExistingLog(_userLog);
                if (existLogCount != 0)
                {
                    throw new Exception();
                }
                PushLogToDB(_userLog);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public UserLog MakeLog(DateTime dateTime, string category, LogLevel logLevel, int userId, string message)
        {
            _userLog.logId = _logDataAccess.GetCurrentUserIdentity() + 1;
            _userLog.dateTime = dateTime;
            _userLog.category = category;
            _userLog.logLevel = logLevel;
            _userLog.userId = userId;
            _userLog.message = message;

            return _userLog;
        }

        public bool PushLogToDB(UserLog sysLog)
        {
            try
            {
                _logDataAccess.PushLogToDB(sysLog);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
