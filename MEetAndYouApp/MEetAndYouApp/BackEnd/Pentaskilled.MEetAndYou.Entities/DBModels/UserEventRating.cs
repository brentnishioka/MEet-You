using System;
using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class UserEventRating
    {
        public int EventId { get; set; }
        public int ItineraryId { get; set; }
        public int UserRating { get; set; }
    }
}
