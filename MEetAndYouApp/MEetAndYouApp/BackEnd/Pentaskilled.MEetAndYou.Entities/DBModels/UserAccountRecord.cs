using System;
using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class UserAccountRecord
    {
        public UserAccountRecord()
        {
            Itineraries = new HashSet<Itinerary>();
            UserItineraries = new HashSet<UserItinerary>();
            UserTokens = new HashSet<UserToken>();
            Roles = new HashSet<Role>();
        }

        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public byte[] UserPassword { get; set; }
        public string UserPhoneNum { get; set; }
        public string UserRegisterDate { get; set; }
        public bool Active { get; set; }
        public Guid? Salt { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Itinerary> Itineraries { get; set; }
        public virtual ICollection<UserItinerary> UserItineraries { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Role> Roles { get; set; }

        public override string ToString()
        {
            return $"ID: {UserId}\nEmail: {UserEmail}\nPhone Number: {UserPhoneNum}";
        }
    }
}
