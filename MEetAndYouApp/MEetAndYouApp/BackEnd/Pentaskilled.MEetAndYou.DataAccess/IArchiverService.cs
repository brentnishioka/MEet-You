using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.DataAccess
{
    public interface IArchiverService
    {
        bool IsLogOlderThan30Days(string FileName);
        bool GetOldLogs();
    }
}
