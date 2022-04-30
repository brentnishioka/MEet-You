using System;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class EventLog
    {
        public int LogId { get; set; }
        public DateTime DateTime { get; set; }
        public string Category { get; set; }
        public string LogLevel { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}
