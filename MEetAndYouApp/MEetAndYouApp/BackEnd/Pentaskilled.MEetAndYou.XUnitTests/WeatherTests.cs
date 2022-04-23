using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Services.Implementation;
using Xunit;

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

        [Fact]
        public void ServiceLatLonTest()
        {
            WeatherDAO weatherDAO = new WeatherDAO();
            WeatherService weatherService = new WeatherService();
            string cityName = "London";
            string countryName = "KY";
            string geoJsonString;
            string[] coordinates;

            geoJsonString = weatherDAO.GeoCoderAPI(cityName, countryName);
            coordinates = weatherService.ParseLatLong(geoJsonString);

            Assert.Equal("37.12898", coordinates[0]);
            Assert.Equal("-84.08327", coordinates[1]);
        }
    }
}