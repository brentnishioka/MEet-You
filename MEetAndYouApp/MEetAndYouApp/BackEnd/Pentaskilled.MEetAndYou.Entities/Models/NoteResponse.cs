using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class NoteResponse : BaseResponse
    {
        private List<ItineraryNote> _data;

        public List<ItineraryNote> Data { get; set; }

        public NoteResponse() : base()
        {
            Data = new List<ItineraryNote>();
        }

        public NoteResponse(string message, bool isSuccessful, List<ItineraryNote> data)
        {
            Message = message;
            IsSuccessful = isSuccessful;
            Data = data;
        }
    }
}
