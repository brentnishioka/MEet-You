using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Entities
{
    public class LogEntity
    {
        public DateTime dateTime { get; set; }
        public string category { get; set; }
        public LogLevel logLevel { get; set; }
        public string username { get; set; }
        public string message { get; set; }
    }
}
