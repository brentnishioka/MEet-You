using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Services.Contracts
{
    public interface IWeatherService
    {
        List<string> CalculateWeather(string cityName, string stateName, DateTime dateTime);

        string[] ParseLatLong(string jsonString);

        List<string> ParseWeatherInfo(string jsonString, DateTime dateTime);
    }
}
