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
using Xunit.Abstractions;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class UPDTests
    {
        private readonly ITestOutputHelper testOutputHelper;


        public UPDTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void checkItineraryDAO()
        {
            IItineraryDAO iDAO = new IItineraryDAO(new MEetAndYouDBContext());

            List<Itinerary> itineraryList = iDAO.GetUserItineraries(1).Result;

            foreach (Itinerary i in itineraryList)
            {
                testOutputHelper.WriteLine(i.ToString()); 
            }


            Assert.NotNull(iDAO.GetUserItineraries(1));


        }














    }
}
