using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class UserEventRating
    {
        public int eventID { get; set; }
        public int itineraryID { get; set; }
        public int userRating { get; set; }
    }
}
