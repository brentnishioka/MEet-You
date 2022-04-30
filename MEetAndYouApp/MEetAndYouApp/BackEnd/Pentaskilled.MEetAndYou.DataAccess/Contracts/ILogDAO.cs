using System.Collections.Generic;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;

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
