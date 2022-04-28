using System;
using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Services.Contracts
{
    public interface IWeatherService
    {
        List<string> BeginWeatherProcess(string cityName, string stateName, DateTime dateTime);

        string[] ParseLatLong(string jsonString);

        List<string> ParseWeatherInfo(string jsonString, DateTime dateTime);
    }
}
