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

        /// <summary>
        /// Executes the flow of control for the system logging process asynchronously.
        /// </summary>
        /// <param name="dateTime"> The UTC date & time the log process is invoked. </param>
        /// <param name="category"> The category of the log. </param>
        /// <param name="logLevel"> The level of the log. </param>
        /// <param name="message"> A message associated with the log. </param>
        /// <returns> Returns true if the system logging process is successful, false if unsuccessful. </returns>
        public async Task<bool> CreateNewLogAsync(DateTime dateTime, string category, LogLevel logLevel, string message)
        {
            return await Task.Run(async () => {
                try
                {
                    await MakeLogAsync(dateTime, category, logLevel, message);
                    int existLogCount = await Task.Run(() => _logDataAccess.CheckExistingLogAsync(_eventLog));
                    if (existLogCount != 0)
                    {
                        throw new Exception();
                    }

                    await PushLogToDBAsync(_eventLog);
                    Log sysLogCheck = await Task.Run(() => _logDataAccess.UpdateLogAsync(_eventLog));
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

        /// <summary>
        /// Executes the flow of control for the user logging process asynchronously.
        /// </summary>
        /// <param name="dateTime"> The UTC date & time the log process is invoked. </param>
        /// <param name="category"> The category of the log. </param>
        /// <param name="logLevel"> The level of the log. </param>
        /// <param name="userId"> The ID of the user performing the operation. </param>
        /// <param name="message"> A message associated with the log. </param>
        /// <returns> Returns true if the user logging process is successful, false if unsuccessful. </returns>
        public async Task<bool> CreateNewLogAsync(DateTime dateTime, string category, LogLevel logLevel, int userId, string message)
        {
            return await Task.Run(async () => {
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

        /// <summary>
        /// Instantiates the fields of the _eventLog instance for system logs asynchronously.
        /// </summary>
        /// <param name="dateTime"> The UTC date & time the log process is invoked. </param>
        /// <param name="category"> The category of the log. </param>
        /// <param name="logLevel"> The level of the log. </param>
        /// <param name="message"> A message associated with the log. </param>
        /// <returns> Returns true if the log is made successfully, false if otherwise. </returns>
        /// <exception cref="Exception"> Exception if an invalid category is thrown. </exception>
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

        /// <summary>
        /// Instantiates the fields of the _eventLog instance for user logs asynchronously.
        /// </summary>
        /// <param name="dateTime"> The UTC date & time the log process is invoked. </param>
        /// <param name="category"> The category of the log. </param>
        /// <param name="logLevel"> The level of the log. </param>
        /// <param name="userId"> The ID of the user performing the operation. </param>
        /// <param name="message"> A message associated with the log. </param>
        /// <returns> Returns true if the log is made successfully, false if otherwise. </returns>
        /// <exception cref="Exception"> Exception if an invalid category is thrown. </exception>
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

        /// <summary>
        /// Helper method to push our created event log to the data access layer asynchronously.
        /// </summary>
        /// <param name="eventLog"> The log object to be transferred to the data access layer. </param>
        /// <returns> Returns true if log is pushed successfully, false if otherwise. </returns>
        public async Task<bool> PushLogToDBAsync(Log eventLog)
        {
            return await Task.Run(() => {
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
