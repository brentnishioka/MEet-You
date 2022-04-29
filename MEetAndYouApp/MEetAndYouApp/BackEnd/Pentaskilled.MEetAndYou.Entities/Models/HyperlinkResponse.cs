using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class HyperlinkResponse : BaseResponse
    {
        private Itinerary _data;

        public Itinerary Data { get; set; }

        public HyperlinkResponse() : base()
        {
            Data = new Itinerary();
        }

        public HyperlinkResponse(string message, bool isSuccessful, Itinerary data)
        {
            Message = message;
            IsSuccessful = isSuccessful;
            Data = data;
        }
    }
}
