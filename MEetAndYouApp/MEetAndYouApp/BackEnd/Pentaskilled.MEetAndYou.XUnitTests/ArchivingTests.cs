using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Logging;
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
           
            Assert.Equal(1, count);
        }


    }
}
