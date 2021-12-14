using Pentaskilled.MEetAndYou.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public interface ILogDAO
    {
        bool PushLogToDB(Log eventLog);
        int GetCurrentIdentity();
        int CheckExistingLog(Log eventLog);
        Log UpdateLog(Log eventLog);

        List<Log> ReadLogsOlderThan30();
        bool DeleteLogsOlderThan30();
        int GetArchiveCount();
    }
}
