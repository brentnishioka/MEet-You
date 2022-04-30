namespace MEetAndYou.EFCoreEntities.Models
{
    public partial class Event
    {
        public Event()
        {
            CategoryNames = new HashSet<Category>();
            Itineraries = new HashSet<Itinerary>();
        }

        public int EventId { get; set; }
        public string? EventName { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public double? Price { get; set; }
        public DateTime? EventDate { get; set; }

        public virtual ICollection<Category> CategoryNames { get; set; }
        public virtual ICollection<Itinerary> Itineraries { get; set; }
    }
}
