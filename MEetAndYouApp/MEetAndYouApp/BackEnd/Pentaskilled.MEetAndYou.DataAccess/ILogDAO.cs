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
        Task<bool> PushLogToDBAsync(Log eventLog);
        Task<int> GetCurrentIdentityAsync();
        Task<int> CheckExistingLogAsync(Log eventLog);
        Task<Log> UpdateLogAsync(Log eventLog);

        List<Log> ReadLogsOlderThan30();
        bool DeleteLogsOlderThan30();
        int GetArchiveCount();
    }
}
