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
        private readonly IOpenWeatherWrapper _weatherDAO;
        private string[] _coordinates;
        private List<string> _weatherData;

        public WeatherService()
        {
            _weatherDAO = new OpenWeatherWrapper();
        }

        public List<string> CalculateWeather(string cityName, string stateName, DateTime dateTime)
        {
            string geoJsonString = _weatherDAO.GetGeoCoords(cityName, stateName);
            _coordinates = ParseLatLong(geoJsonString);

            string weatherJsonString = _weatherDAO.GetWeatherData(_coordinates[0], _coordinates[1]);
            _weatherData = ParseWeatherInfo(weatherJsonString, dateTime);

            return _weatherData;
        }

        public string[] ParseLatLong(string jsonString)
        {
            WeatherForecast weatherForecast =       // Deseralize JSON string to WeatherForecast entity
                JsonSerializer.Deserialize<WeatherForecast>(jsonString);

            _coordinates = new string[] { weatherForecast.lat.ToString(), weatherForecast.lon.ToString() };

            return _coordinates;
        }

        public List<string> ParseWeatherInfo(string jsonString, DateTime dateTime)
        {
            WeatherForecast weatherForecast =       // Deseralize JSON string to WeatherForecast entity
                JsonSerializer.Deserialize<WeatherForecast>(jsonString);

            // TODO: Use dateTime to get temp and weather type for correct day

            foreach (var day in weatherForecast.daily)  // Iterate through dictionary/day
            {
                Console.WriteLine(day["dt"]);
                Console.WriteLine(day["temp"]);
                Console.WriteLine(day["weather"]);
                Console.WriteLine();
            }

            return _weatherData;
        }



    }
}
