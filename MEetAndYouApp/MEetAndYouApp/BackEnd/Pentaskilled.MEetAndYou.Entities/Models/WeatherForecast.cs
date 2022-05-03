using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities
{
    public class WeatherForecast
    {
        public float lat { get; set; }
        public float lon { get; set; }
        public List<Dictionary<string, object>> daily { get; set; }
    }
}
