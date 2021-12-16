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
        public void ArchiveOldLogsTest()
        {
            ArchiveManager archMan = new ArchiveManager();
            bool archOldLogsExpected = true;

            Assert.Equal(archOldLogsExpected, archMan.ArchiveOldLogs());
        }
    }
}
