using Xunit;
using System;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class WeatherTests
    {
        [Fact]
        public void GetLatLonTest()
        {
            WeatherDAO weatherDAO = new WeatherDAO();
            string cityName = "London";
            string countryName = "KY";
            string[] coordinates;

            coordinates = weatherDAO.GeoCoderAPI(cityName, countryName);

            Assert.Equal("37.12898", coordinates[0]);
            Assert.Equal("-84.08327", coordinates[1]);
        }
    }
}