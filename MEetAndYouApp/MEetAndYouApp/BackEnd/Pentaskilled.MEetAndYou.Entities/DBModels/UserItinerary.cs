using System;
using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class UserItinerary
    {
        public UserItinerary()
        {
            // Default constructor required for EF
        }

        public UserItinerary(int userId, int itineraryID, string permissionName)
        {
            UserId = userId;
            ItineraryId = itineraryID;
            PermissionName = permissionName;
        }

        public int ItineraryId { get; set; }
        public int UserId { get; set; }
        public string PermissionName { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Itinerary Itinerary { get; set; }
        public virtual Permission PermissionNameNavigation { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual UserAccountRecord User { get; set; }
    }
}
