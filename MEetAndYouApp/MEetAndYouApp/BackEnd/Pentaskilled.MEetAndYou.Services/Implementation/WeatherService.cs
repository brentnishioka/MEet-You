using System;
using System.Collections.Generic;
using System.Text.Json;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Entities;
using Pentaskilled.MEetAndYou.Services.Contracts;

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
            _coordinates = ParseLatLong(geoJsonString);

            string weatherJsonString = _weatherDAO.OneCallAPI(_coordinates[0], _coordinates[1]);
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
