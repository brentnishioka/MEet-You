using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Entities;
using System.Text.Json;

namespace Pentaskilled.MEetAndYou.Services.Implementation
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherDAO _weatherDAO;
        private string[] _coordinates;
        private List<string> _weatherData;

        public WeatherService()
        {
            _weatherDAO = new WeatherDAO();
        }

        public List<string> BeginWeatherProcess(string cityName, string stateName, DateTime dateTime)
        {
            string geoJsonString = _weatherDAO.GeoCoderAPI(cityName, stateName);

            return _weatherData;
        }

        public string[] ParseLatLong(string jsonString)
        {
            WeatherForecast weatherForecast =       // Deseralize JSON string to WeatherForecast entity
                JsonSerializer.Deserialize<WeatherForecast>(jsonString);

            _coordinates = new string[] { weatherForecast.lat.ToString(), weatherForecast.lon.ToString() };

            return _coordinates;
        }

    }
}
