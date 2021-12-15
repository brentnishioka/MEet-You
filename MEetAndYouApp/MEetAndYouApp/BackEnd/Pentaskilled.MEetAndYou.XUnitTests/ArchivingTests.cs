using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Logging;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Services;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementations;
using Xunit;


namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class ArchivingTests
    {

        [Fact]
        public void GetArchiveCountTest()
        { 

            ILogDAO logDAO = new LogDAO();
            int count = logDAO.GetArchiveCount();
            int expectedArchCount = 1;
           
            Assert.Equal(expectedArchCount, count);
        }

        [Fact]
        public void DeleteLogsOlderThan30Test()
        {
            ILogDAO logDAO = new LogDAO();
            bool endCondition = logDAO.DeleteLogsOlderThan30();
            bool isSuccessfullyDeleted = true;

            Assert.Equal(isSuccessfullyDeleted, endCondition);
        }

        [Fact]
        public void ReadLogsOlderThan30Test()
        {
            ILogDAO logDAO = new LogDAO();
            List<Log> oldLogs = logDAO.ReadLogsOlderThan30();
            int expectedOldLogsCount = 1;

            Assert.Equal(expectedOldLogsCount, oldLogs.Count);
        }

        [Fact]
        public void GetOldLogsTest()
        {
            IArchiverService archiverService = new ArchiverService(new LogDAO());
            List<Log> oldLogs = archiverService.GetOldLogs();
            int expectedOldLogsCount = 1;

            Assert.Equal(expectedOldLogsCount, oldLogs.Count);
        }

        [Fact]
        public void ConsolidateOldLogsTest()
        {
            IArchiverService archiverService = new ArchiverService(new LogDAO());
            List<Log> oldLogs = archiverService.GetOldLogs();
            bool returnValue = archiverService.ConsolidateOldLogs(oldLogs);

            Assert.True(returnValue);
        }

        // running this test in a row might lead to an error because the file would 
        // already exist in the directory
        [Fact]
        public void CompressOldLogsTest()
        {
            IArchiverService archiverService = new ArchiverService(new LogDAO());
            bool returnValue = archiverService.CompressOldLogs();

            Assert.True(returnValue);
        }


        // Remember that this test will affect your local db and can affect other tests
        [Fact]
        public void DeleteOldLogsTest()
        {
            IArchiverService archiverService = new ArchiverService(new LogDAO());
            List<Log> oldLogs = archiverService.GetOldLogs();
            archiverService.DeleteOldLogs(oldLogs); 
            int expectedOldLogsCount = 0;
            oldLogs = archiverService.GetOldLogs();

            Assert.Equal(expectedOldLogsCount, oldLogs.Count);
        }

        [Fact]
        public void ArchiveOldLogsTest()
        {
            ArchiveManager archMan = new ArchiveManager();
            bool archOldLogsExpected = true;

            Assert.Equal(archOldLogsExpected, archMan.ArchiveOldLogs());
        }
    }
}
