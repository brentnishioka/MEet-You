using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.Logging
{
    public class SystemLoggingService : ISystemLoggingService
    {
        private readonly ILogDAO _logDataAccess;
        private SystemLog _sysLog;

        public SystemLoggingService()
        { 
            _logDataAccess = new LogDAO();
            _sysLog = new SystemLog();
        }

        public bool CreateNewLog(DateTime dateTime, string category, LogLevel logLevel, string message)
        {
            try
            {
                MakeLog(dateTime, category, logLevel, message);
                int existLogCount = _logDataAccess.CheckExistingLog(_sysLog);
                if (existLogCount != 0)
                {
                    throw new Exception();
                }
                PushLogToDB(_sysLog);
                SystemLog sysLogCheck = _logDataAccess.UpdateSysLog(_sysLog);
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

        public SystemLog MakeLog(DateTime dateTime, string category, LogLevel logLevel, string message)
        {
            _sysLog.logId = _logDataAccess.GetCurrentSysIdentity() + 1;
            _sysLog.dateTime = dateTime;
            _sysLog.category = category;
            _sysLog.logLevel = logLevel;
            _sysLog.message = message;
            
            return _sysLog;
        }

        public bool PushLogToDB(SystemLog sysLog)
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
