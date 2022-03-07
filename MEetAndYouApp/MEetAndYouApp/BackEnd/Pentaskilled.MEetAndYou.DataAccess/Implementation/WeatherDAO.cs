using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities;
using System.IO;

namespace Pentaskilled.MEetAndYou.DataAccess.Implementation
{
    public class WeatherDAO : IWeatherDAO
    {
        private string[] _coordinates;
        private List<string> _weatherData;

        // TODO: Move API key as environmental variable or config file 
        private StreamReader _keyReader = new StreamReader("D:\\weather.txt");

        public string GeoCoderAPI(string cityName, string countryName)
        {
            var weatherKey = _keyReader.ReadLine(); // Get API Key

            var geoClient = new HttpClient();
            var geoRequest = new HttpRequestMessage(HttpMethod.Get, $"http://api.openweathermap.org/geo/1.0/direct?q={cityName},{countryName},US&limit=5&appid={weatherKey}");
            var geoResponse = geoClient.Send(geoRequest);

            geoResponse.EnsureSuccessStatusCode(); // Throw exception for HTTP response failure

            var geoJsonString = geoResponse.Content.ReadAsStringAsync().Result;

            geoJsonString = geoJsonString.Substring(1, geoJsonString.Length - 2);

            return geoJsonString;
        }
    }
}
