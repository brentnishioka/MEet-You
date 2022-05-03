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
        private List<UserItinerary> _data;

        public List<UserItinerary> Data { get; set; }

        public HyperlinkResponse() : base()
        {
            Data = new List<UserItinerary>();
        }

        public HyperlinkResponse(string message, bool isSuccessful, List<UserItinerary> data)
        {
            Message = message;
            IsSuccessful = isSuccessful;
            Data = data;
        }
    }
}
