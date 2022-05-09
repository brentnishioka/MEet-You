using System;
using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class UserEventRating
    {
        public UserEventRating()
        {

        }

        public UserEventRating(int eventID, int itineraryID, int userRating)
        {
            EventId = eventID;
            ItineraryId = itineraryID;
            UserRating = userRating;
        }

        public int EventId { get; set; }
        public int ItineraryId { get; set; }
        public int UserRating { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Event Event { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Itinerary Itinerary { get; set; }
    }
}
