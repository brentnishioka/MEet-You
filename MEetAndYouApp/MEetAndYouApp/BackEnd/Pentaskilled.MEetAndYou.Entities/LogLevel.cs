using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Entities
{
    public enum LogLevel
    {
        Info,
        Debug,
        Warning,
        Error
    }
    
    public class LogLvl
    {
        public Dictionary<string, LogLevel> _loglvl { get { return _loglvl; } set { } }


        public LogLvl()
        {
            _loglvl = new Dictionary<string, LogLevel>()
            {
                {"Info", LogLevel.Info},
                {"Debug", LogLevel.Debug},
                {"Warning", LogLevel.Warning},
                {"Error", LogLevel.Error},

            };
        }


    }
}
