using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Services.Contracts;
using Pentaskilled.MEetAndYou.Services.Implementations;

namespace Pentaskilled.MEetAndYou.Managers
{
    public class WeatherManager
    {
        private readonly IWeatherService _weatherService;
        private List<string> _weatherData;

        public WeatherManager()
        {
            _weatherService = new WeatherService();
        }

        public List<string> GetWeather(string cityName, string stateName, DateTime dateTime)
        {
            try
            {
                _weatherData = _weatherService.BeginWeatherProcess(cityName, stateName, dateTime);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (_weatherData != null)
            {
                return _weatherData;
            }
            throw new NullReferenceException();
        }
    }
}
