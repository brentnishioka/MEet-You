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
        public async Task<bool> CreateNewLogAsync(DateTime dateTime, string category, LogLevel logLevel, string message)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    await MakeLogAsync(dateTime, category, logLevel, message);
                    int existLogCount = await Task.Run(() => _logDataAccess.CheckExistingLogAsync(_eventLog));
                    if (existLogCount != 0)
                    {
                        throw new Exception();
                    }
                    await PushLogToDBAsync(_eventLog);
                    Log sysLogCheck = await Task.Run (() => _logDataAccess.UpdateLogAsync(_eventLog));
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
            });
        }

        // User event log
        public async Task<bool> CreateNewLogAsync(DateTime dateTime, string category, LogLevel logLevel, int userId, string message)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    await MakeLogAsync(dateTime, category, logLevel, userId, message);
                    int existLogCount = await Task.Run(() => _logDataAccess.CheckExistingLogAsync(_eventLog));
                    if (existLogCount != 0)
                    {
                        throw new Exception();
                    }
                    await PushLogToDBAsync(_eventLog);
                    Log eventLog = await Task.Run(() => _logDataAccess.UpdateLogAsync(_eventLog));
                    if (eventLog != null)
                        throw new Exception();
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            });
        }

        // System event log
        public async Task<Log> MakeLogAsync(DateTime dateTime, string category, LogLevel logLevel, string message)
        {
            _eventLog.logId = await Task.Run(() => _logDataAccess.GetCurrentIdentityAsync()) + 1;
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
        public async Task<Log> MakeLogAsync(DateTime dateTime, string category, LogLevel logLevel, int userId, string message)
        {
            _eventLog.logId = await Task.Run(() => _logDataAccess.GetCurrentIdentityAsync()) + 1;
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

        public async Task<bool> PushLogToDBAsync(Log eventLog)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _logDataAccess.PushLogToDBAsync(eventLog);
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            });
        }
    }
}
