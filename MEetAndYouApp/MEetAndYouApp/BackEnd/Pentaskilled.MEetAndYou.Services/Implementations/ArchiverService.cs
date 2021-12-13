using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class ArchiverService : IArchiverService
    {
        public void Compress(string buffLocation, string archiveLocation)
        {
            throw new NotImplementedException();
        }

        public bool CompressOldLogs()
        {
            throw new NotImplementedException();
        }

        public void Consolidate(List<Log> oldLogs)
        {
            throw new NotImplementedException();
        }

        public bool ConsolidateOldLogs(List<Log> oldLogs)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOldLogs(List<Log> oldLogs)
        {
            throw new NotImplementedException();
        }

        public bool GetOldLogs()
        {
            throw new NotImplementedException();
        }

        public bool IsLogOlderThan30Days(string FileName)
        {
            throw new NotImplementedException();
        }
    }
}
