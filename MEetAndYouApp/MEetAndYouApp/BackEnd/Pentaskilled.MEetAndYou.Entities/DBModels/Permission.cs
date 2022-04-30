using System;
using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class Permission
    {
        public Permission()
        {
            UserItineraries = new HashSet<UserItinerary>();
        }

        public string PermissionName { get; set; }

        public virtual ICollection<UserItinerary> UserItineraries { get; set; }
    }
}
