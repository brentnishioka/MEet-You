using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Services.Contracts
{
    public interface IWeatherService
    {
        List<string> BeginWeatherProcess(string cityName, string stateName, DateTime dateTime);

        string[] ParseLatLong(string jsonString);
    }
}
