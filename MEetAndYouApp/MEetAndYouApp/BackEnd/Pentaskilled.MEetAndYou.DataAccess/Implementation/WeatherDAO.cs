using System.Configuration;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities;
using System.IO;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class WeatherDAO : IWeatherDAO
    {
        private string[] _coordinates;
        // TODO: Move API key as environmental variable or config file 
        private StreamReader _keyReader = new StreamReader("D:\\weather.txt");

        public string[] GeoCoderAPI(string cityName, string countryName)
        {
            var weatherKey = _keyReader.ReadLine();

            var GeoClient = new HttpClient();

            var GeoRequest = new HttpRequestMessage(HttpMethod.Get, $"http://api.openweathermap.org/geo/1.0/direct?q={cityName},{countryName},US&limit=5&appid={weatherKey}");

            var GeoResponse = GeoClient.Send(GeoRequest);
            GeoResponse.EnsureSuccessStatusCode(); // Throw an exception if error

            var WeatherBody = GeoResponse.Content.ReadAsStringAsync().Result;

            WeatherBody = WeatherBody.Substring(1, WeatherBody.Length - 2);

            WeatherForecast weatherForecast =
                JsonSerializer.Deserialize<WeatherForecast>(WeatherBody);

            _coordinates = new string[] { weatherForecast.lat.ToString(), weatherForecast.lon.ToString() };

            return _coordinates;
        }
    }
}
