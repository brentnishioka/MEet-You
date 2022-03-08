using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Entities
{
    public class WeatherForecast
    {
        public float lat { get; set; }
        public float lon { get; set; }
        public List<Dictionary<string, object>> daily { get; set; }
    }
}
