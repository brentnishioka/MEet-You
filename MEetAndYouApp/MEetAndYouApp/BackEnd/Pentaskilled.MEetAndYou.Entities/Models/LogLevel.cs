using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities
{
    public enum LogLevel
    {
        Info,
        Debug,
        Warning,
        Error
    }

    public class LogLevelDict
    {
        public Dictionary<string, LogLevel> logLvlDict { get; set; }

        public LogLevelDict()
        {
            logLvlDict = new Dictionary<string, LogLevel>()
            {
                {"Info", LogLevel.Info},
                {"Debug", LogLevel.Debug},
                {"Warning", LogLevel.Warning},
                {"Error", LogLevel.Error}
            };
        }
    }
}
