namespace MEetAndYou.EFCoreEntities.Models
{
    public partial class UserAccountRecord
    {
        public UserAccountRecord()
        {
            Itineraries = new HashSet<Itinerary>();
            UserTokens = new HashSet<UserToken>();
            ItinerariesNavigation = new HashSet<Itinerary>();
            Roles = new HashSet<Role>();
        }

        public int UserId { get; set; }
        public string? UserEmail { get; set; }
        public byte[] UserPassword { get; set; } = null!;
        public string UserPhoneNum { get; set; } = null!;
        public string UserRegisterDate { get; set; } = null!;
        public bool Active { get; set; }
        public Guid? Salt { get; set; }

        public virtual ICollection<Itinerary> Itineraries { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }

        public virtual ICollection<Itinerary> ItinerariesNavigation { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
