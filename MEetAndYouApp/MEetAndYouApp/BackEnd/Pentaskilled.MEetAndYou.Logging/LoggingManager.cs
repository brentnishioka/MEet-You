using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Logging;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.Logging
{
    public class LoggingManager
    {
        private ISystemLoggingService _sysLogServ;
        private IUserLoggingService _userLogServ;

        public LoggingManager()
        {
            _sysLogServ = new SystemLoggingService();
            _userLogServ = new UserLoggingService();
        }

        /// <summary>
        /// Begins the process to create a system log.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool BeginLogProcess(string category, LogLevel logLevel, string message)
        {
            try
            {
                DateTime currentDateTime = DateTime.UtcNow;
                bool isCreateLogSuccess = _sysLogServ.CreateNewLog(currentDateTime, category, logLevel, message);
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
        }

        /// <summary>
        /// Begins the process to create a user log.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="logLevel"></param>
        /// <param name="userId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool BeginLogProcess(string category, LogLevel logLevel, int userId, string message)
        {
            try
            {
                DateTime currentDateTime = DateTime.UtcNow;
                bool isCreateLogSuccess = _userLogServ.CreateNewLog(currentDateTime, category, logLevel, userId, message);
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
        }
    }
}
