using System;
using System.Collections.Generic;

namespace MEetAndYou.EFCoreEntities.Models
{
    public partial class Itinerary
    {
        public Itinerary()
        {
            Users = new HashSet<UserAccountRecord>();
        }

        public int ItineraryId { get; set; }
        public string ItineraryName { get; set; } = null!;
        public int Rating { get; set; }
        public int ItineraryOwner { get; set; }

        public virtual UserAccountRecord ItineraryOwnerNavigation { get; set; } = null!;

        public virtual ICollection<UserAccountRecord> Users { get; set; }
    }
}
