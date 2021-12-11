﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Entities
{
    public class UserLog
    {
        public int logId { get; }
        public DateTime dateTime { get; set; }
        public string category { get; set; }
        public LogLevel logLevel { get; set; }
        public int userId { get; set; }
        public string message { get; set; }
    }
}
