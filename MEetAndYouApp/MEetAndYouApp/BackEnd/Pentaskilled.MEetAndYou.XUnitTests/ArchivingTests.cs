using Pentaskilled.MEetAndYou.Managers;
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
