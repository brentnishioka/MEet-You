﻿using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.DomainModels;

namespace Pentaskilled.MEetAndYou.Services.Implementations
{
    public class ArchiverService : IArchiverService
    {

        private ILogDAO _logDataAccess;


        public ArchiverService(ILogDAO logDAO)
        {
            _logDataAccess = logDAO;
        }

        ///<summary>
        /// Helper method that reads in the .csv file(s) of logs from
        /// buffLocation, zips the file(s), and stores them in archiveLocation
        /// </summary>
        /// <param name="buffLocation"></param>
        /// <param name="archiveLocation"></param>
        /// <returns></returns>
        public void Compress(string buffLocation, string archiveLocation)
        {
            ArchiveConfig archConf = new ArchiveConfig();
            string fileName = DateTime.Now.ToString(archConf.GetDateTimeFormat()) +
                                      archConf.GetArchiveExtension();
            DirectoryInfo di = new DirectoryInfo(buffLocation);
            List<FileInfo> files = di.GetFiles("*.csv").ToList();
            string zipPath = archiveLocation + @"\" + fileName;

            if (!Directory.Exists(buffLocation))
            {
                Directory.CreateDirectory(buffLocation);
            }

            if (!Directory.Exists(archiveLocation))
            {
                Directory.CreateDirectory(archiveLocation);
            }

            foreach (FileInfo file in files)
            {
                using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile(file.ToString(), Path.GetFileName(file.ToString()));
                }
            }
        }

        public bool CompressOldLogs()
        {
            try
            {
                ArchiveConfig archConf = new ArchiveConfig();
                var buffLocation = archConf.GetBufferLocation();
                var archiveLocation = archConf.GetArchiveLocation();

                if (!Directory.Exists(buffLocation))
                {
                    Directory.CreateDirectory(buffLocation);
                }

                if (!Directory.Exists(archiveLocation))
                {
                    Directory.CreateDirectory(archiveLocation);
                }

                Compress(buffLocation, archiveLocation);
                string archiveFileExt = DateTime.Now.ToString(archConf.GetDateTimeFormat()) +
                          archConf.GetArchiveExtension();
                if (!File.Exists(archiveFileExt))
                {
                    throw new Exception();
                }
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
            string fileName = DateTime.Now.ToString(archConf.GetDateTimeFormat()) + archConf.GetBufferExtension();
            string completePath = $"{buffLocation}" + "\\" + $"{fileName}";


            if (!Directory.Exists(buffLocation))
            {
                Directory.CreateDirectory(buffLocation);
            }

            using (StreamWriter sw = File.CreateText(completePath))
            {
                foreach (Log log in oldLogs)
                    sw.WriteLine(log.ToString());
            }   
        }

        public bool ConsolidateOldLogs(List<Log> oldLogs)
        {
            try
            {
                ArchiveConfig archConf = new ArchiveConfig();
                var buffLocation = archConf.GetBufferLocation();

                if (!Directory.Exists(buffLocation))
                {
                    Directory.CreateDirectory(buffLocation);
                }

                Consolidate(oldLogs, buffLocation);
                string fileName = DateTime.Now.ToString(archConf.GetDateTimeFormat()) + archConf.GetBufferExtension();
                string completePath = $"{buffLocation}" + "\\" + $"{fileName}";
                int csvRowCount = 0;
                string line;
                StreamReader file = new StreamReader(completePath); 
                while ((line = file.ReadLine()) != null)
                        csvRowCount++;
                file.Close();
                if (oldLogs.Count != csvRowCount)
                    throw new Exception();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        // can more error handling be done?
        public bool DeleteOldLogs(List<Log> oldLogs)
        {
            _logDataAccess = new LogDAO();
            int deleteCount = _logDataAccess.GetArchiveCount();
            if (oldLogs.Count != deleteCount)
            {
                return false;
            }
            return _logDataAccess.DeleteLogsOlderThan30();
        }

        public List<Log> GetOldLogs()
        {
            List<Log> oldLogs = _logDataAccess.ReadLogsOlderThan30();
            bool isEmpty = oldLogs?.Any() != true;
            if (isEmpty)
            {
                return null;
            }
            else
            {
                int archiveCount = _logDataAccess.GetArchiveCount();
                if (archiveCount != oldLogs.Count)
                    return null;
                return oldLogs;
            }

        }

    }
}
