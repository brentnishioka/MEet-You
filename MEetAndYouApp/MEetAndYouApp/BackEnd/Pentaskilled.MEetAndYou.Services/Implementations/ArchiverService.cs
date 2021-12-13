using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.DomainModels;

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
            try
            {
                ArchiveConfig archConf = new ArchiveConfig();
                var buffLocation = archConf.GetBufferLocation();
                var archiveLocation = archConf.GetArchiveLocation();
                Compress(buffLocation, archiveLocation);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        ///<summary>
        /// Helper Method that takes in old logs from DB and appends them to .csv file
        /// NOTE: LOCATION WAS MADE ON RANDOM FOLDER ON MY MACHINE FOR 
        /// TESTING PURPOSES change in ArchiveConfig.cs BUFFER_LOCATION 
        /// and ARCHIVE_LOCATION
        /// </summary>
        /// <param name="oldLogs"></param>
        /// <param name="buffLocation"></param>
        /// <returns></returns>
        public void Consolidate(List<Log> oldLogs, string buffLocation)
        {
            ArchiveConfig archConf = new ArchiveConfig();
            string fileName = DateTime.Now.ToString(archConf.GetDateTimeFormat()) + archConf.GetArchiveExtension();
            string completePath = $"{buffLocation}" + "\\" + $"{fileName}";

            foreach(Log log in oldLogs)
            {
                using (StreamWriter sw = File.CreateText(completePath))
                {
                    sw.WriteLine(log.ToString());
                }
            }
        }

        public bool ConsolidateOldLogs(List<Log> oldLogs)
        {
            try
            {
                ArchiveConfig archConf = new ArchiveConfig();
                var buffLocation = archConf.GetBufferLocation();
                Consolidate(oldLogs, buffLocation);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool DeleteOldLogs(List<Log> oldLogs)
        {
            throw new NotImplementedException();
        }

        public List<Log> GetOldLogs()
        {
            return new LogDAO().ReadLogsOlderThan30();
        }

        public bool IsLogOlderThan30Days(string FileName)
        {
            throw new NotImplementedException();
        }
    }
}
