using Xunit;
using System;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class WeatherTests
    {
        [Fact]
        public void DAOLatLonTest()
        {
            WeatherDAO weatherDAO = new WeatherDAO();
            string cityName = "London";
            string countryName = "KY";
            string geoJsonString;

            geoJsonString = weatherDAO.GeoCoderAPI(cityName, countryName);

            Assert.Equal("{\"name\":\"London\",\"lat\":37.1289771,\"lon\":-84.0832646,\"country\":\"US\",\"state\":\"Kentucky\"}", geoJsonString);
        }
    }
}