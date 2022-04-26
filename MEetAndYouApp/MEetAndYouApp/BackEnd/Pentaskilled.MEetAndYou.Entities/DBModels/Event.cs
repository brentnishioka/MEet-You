using System;
using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class Event
    {
        public Event()
        {
            CategoryNames = new HashSet<Category>();
            Itineraries = new HashSet<Itinerary>();
        }

        public Event(int newID, string name, string description, string address, float price, DateTime dateTime)
        {
            EventId = newID;
            EventName = name;
            Description = description;
            Address = address;
            Price = price;
            EventDate = dateTime;
        }

        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double? Price { get; set; }
        public DateTime? EventDate { get; set; }

        // Temporary method to solve circular reference between Itinerary and Event
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Category> CategoryNames { get; set; }
        public virtual ICollection<Itinerary> Itineraries { get; set; }
    }
}
