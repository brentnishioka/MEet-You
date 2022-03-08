using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentaskilled.MEetAndYou.Entities
{
    public class Itinerary
    {
        private float _rating;
        public int ItineraryID { get; set; }
        public string Name { get; set; }
        public float Rating { 
            get { return _rating; }
            set {
                if (value >= 0)
                {
                    _rating = value;
                }
            }
        }
    }
}
