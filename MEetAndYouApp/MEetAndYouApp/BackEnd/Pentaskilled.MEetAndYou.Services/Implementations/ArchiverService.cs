using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public class ArchiverService : IArchiverService
    {

        public bool GetOldLogs()
        {
            throw new NotImplementedException();
        }

        public bool IsLogOlderThan30Days(string FileName)
        {
            throw new NotImplementedException();

        }

        public bool ConsolidateOldLogs(List<Object> oldLogs)
        {
            throw new NotImplementedException();

        }

        public bool CompressOldLogs()
        {
            throw new NotImplementedException();

        }

        public bool RelocateOldLogs()
        {
            throw new NotImplementedException();

        }

        public int GetArchiveCount()
        {
            throw new NotImplementedException();

        }

    }
}
