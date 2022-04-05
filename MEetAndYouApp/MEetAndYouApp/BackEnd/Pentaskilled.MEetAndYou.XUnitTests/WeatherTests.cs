using Xunit;
using System;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Services.Implementation;

namespace Pentaskilled.MEetAndYou.XUnitTests
{
    public class WeatherTests
    {
        [Fact]
        public void DAOLatLonTest()
        {
            OpenWeatherWrapper weatherDAO = new OpenWeatherWrapper();
            string cityName = "London";
            string countryName = "KY";
            string geoJsonString;

            geoJsonString = weatherDAO.GetGeoCoords(cityName, countryName);

            Assert.Equal("{\"name\":\"London\",\"lat\":37.1289771,\"lon\":-84.0832646,\"country\":\"US\",\"state\":\"Kentucky\"}", geoJsonString);
        }

        [Fact]
        public void ServiceLatLonTest()
        {
            OpenWeatherWrapper weatherDAO = new OpenWeatherWrapper();
            WeatherService weatherService = new WeatherService();
            string cityName = "London";
            string countryName = "KY";
            string geoJsonString;
            string[] coordinates;

            geoJsonString = weatherDAO.GetGeoCoords(cityName, countryName);
            coordinates = weatherService.ParseLatLong(geoJsonString);

            Assert.Equal("37.12898", coordinates[0]);
            Assert.Equal("-84.08327", coordinates[1]);
        }
    }
}