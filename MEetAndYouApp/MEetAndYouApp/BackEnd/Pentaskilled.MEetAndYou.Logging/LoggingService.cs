using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.Logging
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogDAO _logDataAccess;
        private readonly Log _eventLog;

        public LoggingService(ILogDAO _logDataAccess, Log _eventLog)
        {
            this._logDataAccess = _logDataAccess;
            this._eventLog = _eventLog;
        }

        // System event log
        public bool CreateNewLog(DateTime dateTime, string category, LogLevel logLevel, string message)
        {
            try
            {
                MakeLog(dateTime, category, logLevel, message);
                int existLogCount = _logDataAccess.CheckExistingLog(_eventLog);
                if (existLogCount != 0)
                {
                    throw new Exception();
                }
                PushLogToDB(_eventLog);
                Log sysLogCheck = _logDataAccess.UpdateLog(_eventLog);
                if (sysLogCheck != null)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        // User event log
        public bool CreateNewLog(DateTime dateTime, string category, LogLevel logLevel, int userId, string message)
        {
            try
            {
                MakeLog(dateTime, category, logLevel, userId, message);
                int existLogCount = _logDataAccess.CheckExistingLog(_eventLog);
                if (existLogCount != 0)
                {
                    throw new Exception();
                }
                PushLogToDB(_eventLog);
                Log eventLog = _logDataAccess.UpdateLog(_eventLog);
                if (eventLog != null)
                    throw new Exception();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        // System event log
        public Log MakeLog(DateTime dateTime, string category, LogLevel logLevel, string message)
        {
            _eventLog.logId = _logDataAccess.GetCurrentIdentity() + 1;
            _eventLog.dateTime = dateTime;
            if (!_eventLog.GetValidCategories().Contains(category))
            {
                throw new Exception();
            }
            _eventLog.category = category;
            _eventLog.logLevel = logLevel;
            _eventLog.userId = 0;   // System logs have a userId of 0
            _eventLog.message = message;

            return _eventLog;
        }

        // User event log
        public Log MakeLog(DateTime dateTime, string category, LogLevel logLevel, int userId, string message)
        {
            _eventLog.logId = _logDataAccess.GetCurrentIdentity() + 1;
            _eventLog.dateTime = dateTime;
            if (!_eventLog.GetValidCategories().Contains(category))
            {
                throw new Exception();
            }
            _eventLog.category = category;
            _eventLog.logLevel = logLevel;
            _eventLog.userId = userId;
            _eventLog.message = message;

            return _eventLog;
        }

        public bool PushLogToDB(Log eventLog)
        {
            try
            {
                _logDataAccess.PushLogToDB(eventLog);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
