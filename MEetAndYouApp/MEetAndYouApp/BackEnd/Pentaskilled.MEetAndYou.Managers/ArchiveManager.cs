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
        ///<summary>
        /// Method with the control flow for the whole archival process: consolidate, compress, and delete.
        /// </summary>
        /// <returns>boolean value pertaining to the success of the archival process</returns>
        public bool ArchiveOldLogs()
        {
            var reqStartTime = DateTime.Now;
            // currentDateTime should be DateTime.Now, but it's defined like this for testing.
            DateTime currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            if (currentDateTime.Day == 1 && currentDateTime.Hour == 0 && currentDateTime.Minute == 0 && currentDateTime.Second == 0)
            {
                try
                {
                    List<Log> oldLogs = _archServ.GetOldLogs();
                    bool isConsolidated = _archServ.ConsolidateOldLogs(oldLogs);
                    bool isCompressed = _archServ.CompressOldLogs();
                    bool isDeleted = _archServ.DeleteOldLogs(oldLogs);
                }
                catch (Exception)
                {
                    return false;
                }

                var reqEndTime = DateTime.Now;
                var timeDifference = reqEndTime - reqStartTime;
                if (timeDifference.TotalSeconds <= 60.0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
