using System;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementations;
using Pentaskilled.MEetAndYou.Entities;
using System.Collections.Generic;
using Pentaskilled.MEetAndYou.DataAccess;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class ArchiveManager
    {
        private readonly IArchiverService _archServ;

        public ArchiveManager()
        {
            _archServ = new ArchiverService(new LogDAO());
        }
        
        public bool ArchiveOldLogs()
        {
            DateTime currentDateTime = DateTime.Now;
            /*if (currentDateTime.Day == 1 && currentDateTime.Hour == 0 && currentDateTime.Minute == 0 && currentDateTime.Second == 0)
            {*/
            try
            {
                List<Log> oldLogs = _archServ.GetOldLogs();
                bool isConsolidated = _archServ.ConsolidateOldLogs(oldLogs);
                bool isCompressed = _archServ.CompressOldLogs();
                bool isDeleted = _archServ.DeleteOldLogs(oldLogs);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
/*            }
            return false;*/
        }
    }
}
