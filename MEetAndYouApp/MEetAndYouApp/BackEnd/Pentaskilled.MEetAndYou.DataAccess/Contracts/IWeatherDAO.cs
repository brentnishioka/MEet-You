using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.DataAccess.Contracts
{
    public interface IWeatherDAO
    {
        string GeoCoderAPI(string cityName, string countryName);

        string OneCallAPI(string latitude, string longitude);
    }
}
