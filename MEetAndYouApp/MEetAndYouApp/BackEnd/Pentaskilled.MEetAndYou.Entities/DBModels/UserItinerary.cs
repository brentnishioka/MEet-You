using System;
using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class UserItinerary
    {
        public int ItineraryId { get; set; }
        public int UserId { get; set; }
        public string PermissionName { get; set; }

        public virtual Itinerary Itinerary { get; set; }
        public virtual Permission PermissionNameNavigation { get; set; }
        public virtual UserAccountRecord User { get; set; }
    }
}
