using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.Logging
{
    public interface ISystemLoggingService
    {
        bool CreateNewLog(DateTime dateTime, string category, LogLevel logLevel, string message);
        SystemLog MakeLog(DateTime dateTime, string category, LogLevel logLevel, string message);
        bool PushLogToDB(SystemLog sysLog);
    }
}
