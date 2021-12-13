using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public interface IArchiverService
    {
        bool IsLogOlderThan30Days(string FileName);

        List<Log> GetOldLogs();

        bool ConsolidateOldLogs(List<Log> oldLogs);

        void Consolidate(List<Log> oldLogs);

        bool CompressOldLogs();

        void Compress(string buffLocation, string archiveLocation);

        bool DeleteOldLogs(List<Log> oldLogs);

    }
}
