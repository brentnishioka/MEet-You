using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.Logging
{
    public interface IUserLoggingService
    {
        bool CreateNewLog(DateTime dateTime, string category, LogLevel logLevel, int userId, string message);
        UserLog MakeLog(DateTime dateTime, string category, LogLevel logLevel, int userId, string message);
        bool PushLogToDB(UserLog userLog);
    }
}
