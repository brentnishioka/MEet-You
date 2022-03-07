using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Pentaskilled.MEetAndYou.Services.Contracts;

namespace Pentaskilled.MEetAndYou.Services.Implementations
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherDAO _weatherDAO;
        private List<string> _weatherData;

        public WeatherService()
        {
            _weatherDAO = new WeatherDAO();
        }

        public List<string> BeginWeatherProcess(string cityName, string stateName, DateTime dateTime)
        {
            string[] coordinates = _weatherDAO.GeoCoderAPI(cityName, stateName);

            return _weatherData;
        }
    }
}
