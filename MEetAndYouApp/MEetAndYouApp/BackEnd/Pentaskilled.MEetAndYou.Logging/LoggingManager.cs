using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.Logging
{
    public class LoggingManager
    {
        private readonly ILoggingService _eventLogServ;

        public LoggingManager(ILoggingService _eventLogServ)
        {
            this._eventLogServ = _eventLogServ;
        }

        /// <summary>
        /// Begins & serves as the control flow for the process to create a system event log asynchronously.
        /// </summary>
        /// <param name="category"> The category of the log. </param>
        /// <param name="logLevel"> The level of the log. </param>
        /// <param name="message"> A message associated with the log. </param>
        /// <returns> Returns true if the logging process executed successfully false if otherwise. </returns>
        public async Task<bool> BeginLogProcess(string category, LogLevel logLevel, string message)
        {
            return await Task.Run(async () => {
                try
                {
                    DateTime currentDateTime = DateTime.UtcNow;
                    bool isCreateLogSuccess = await Task.Run(() => _eventLogServ.CreateNewLogAsync(currentDateTime, category, logLevel, message));
                    if (!isCreateLogSuccess)
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
        /// Begins & serves as the control flow for the process to create a system event log asynchronously.
        /// </summary>
        /// <param name="category"> The category of the log. </param>
        /// <param name="logLevel"> The level of the log. </param>
        /// <param name="userId"> The ID of the user performing the operation. </param>
        /// <param name="message"> A message associated with the log. </param>
        /// <returns> Returns true if the logging process executed successfully false if otherwise. </returns>
        public async Task<bool> BeginLogProcess(string category, LogLevel logLevel, int userId, string message)
        {
            return await Task.Run(async () => {
                try
                {
                    DateTime currentDateTime = DateTime.UtcNow;
                    bool isCreateLogSuccess = await Task.Run(() => _eventLogServ.CreateNewLogAsync(currentDateTime, category, logLevel, userId, message));
                    if (!isCreateLogSuccess)
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
    }
}
