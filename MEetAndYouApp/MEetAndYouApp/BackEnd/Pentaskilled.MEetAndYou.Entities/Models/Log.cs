using System;
using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities
{
    public class Log
    {
        public int logId { get; set; }
        public DateTime dateTime { get; set; }
        public string category { get; set; }
        public LogLevel logLevel { get; set; }
        public int userId { get; set; }
        public string message { get; set; }

        private List<string> validCategories;

        public Log()
        {
            validCategories = new List<string>()
            {
                "View", "Business", "Server", "Data", "Data Store"
            };
        }

        public List<string> GetValidCategories()
        {
            return validCategories;
        }


        public override string ToString()
        {
            return $"{logId},{dateTime},{category},{logLevel},{userId},{message}";
        }
    }
}
