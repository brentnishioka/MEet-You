using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Logging;
using Pentaskilled.MEetAndYou.Managers;
using Pentaskilled.MEetAndYou.Services;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Xunit;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class UPDTests
    {


        [Fact]
        public void checkItineraryDAO()
        {
            IItineraryDAO iDAO = new IItineraryDAO(new MEetAndYouDBContext());

            Assert.NotNull(iDAO.GetUserItineraries(1));


        }














    }
}
