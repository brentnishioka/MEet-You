using System;
using System.Collections.Generic;

namespace Pentaskilled.MEetAndYou.Entities.DBModels
{
    public partial class ItineraryNote
    {
        public ItineraryNote()
        {

        }

        public ItineraryNote(int itineraryID, string noteContent)
        {
            ItineraryId = itineraryID;
            NoteContent = noteContent;
        }
        public int ItineraryId { get; set; }
        public string NoteContent { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Itinerary Itinerary { get; set; }
    }
}
