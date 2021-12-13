using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Entities
{
    public class UserLog : Log
    {
        public int userId { get; set; }


        public override string ToString()
        {
            return $"{logId},{dateTime},{category},{logLevel},{userId},{message}";
        }

    }
}
