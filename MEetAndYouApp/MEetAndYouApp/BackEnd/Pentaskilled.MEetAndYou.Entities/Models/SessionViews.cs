using System;

namespace Pentaskilled.MEetAndYou.Entities
{
    public class SessionViews
    {
        public int sessionID { get; set; }
        public string view { get; set; }
        public DateTime timeIn { get; set; }
        public DateTime timeout { get; set; }

        public override string ToString()
        {
            return $"{sessionID}, {view}, {timeIn}, {timeout}";
        }

    }
}
