using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class Itinerary
    {
        public Itinerary()
        {
            Images = new HashSet<Image>();
            UserEventRatings = new HashSet<UserEventRating>();
            UserItineraries = new HashSet<UserItinerary>();
            Events = new HashSet<Event>();
        }

        public int ItineraryId { get; set; }
        public string ItineraryName { get; set; }
        public int Rating { get; set; }
        public int ItineraryOwner { get; set; }

        public virtual UserAccountRecord ItineraryOwnerNavigation { get; set; }
        public virtual ItineraryNote ItineraryNote { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<UserEventRating> UserEventRatings { get; set; }
        public virtual ICollection<UserItinerary> UserItineraries { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
