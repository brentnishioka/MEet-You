using System;
using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class Itinerary
    {
        public Itinerary()
        {
            Users = new HashSet<UserAccountRecord>();
        }

        public int ItineraryId { get; set; }
        public string ItineraryName { get; set; }
        public int Rating { get; set; }
        public int ItineraryOwner { get; set; }

        public virtual UserAccountRecord ItineraryOwnerNavigation { get; set; }

        public virtual ICollection<UserAccountRecord> Users { get; set; }
    }
}
