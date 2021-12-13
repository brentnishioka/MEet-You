using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Entities
{
    public class Log
    {
        public int logId { get; set; }
        public DateTime dateTime { get; set; }
        public string category { get; set; }
        public LogLevel logLevel { get; set; }
        public string message { get; set; }


        public override string ToString()
        {
            return $"{logId},{dateTime},{category},{logLevel},{message}";
        }
    }
}
