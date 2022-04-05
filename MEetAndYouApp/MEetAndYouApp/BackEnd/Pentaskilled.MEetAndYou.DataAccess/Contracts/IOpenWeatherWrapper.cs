using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IOpenWeatherWrapper
    {
        string GetGeoCoords(string cityName, string countryName);

        string GetWeatherData(string latitude, string longitude);
    }
}
