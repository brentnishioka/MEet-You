using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class ItineraryResponse : BaseResponse
    {
        private List<Itinerary> _data;

        public List<Itinerary> Data { get; set; }

        public ItineraryResponse() : base()
        {
            Data = new List<Itinerary>();
        }

        public ItineraryResponse(string message, bool isSuccessful, List<Itinerary> data)
        {
            Message = message;
            IsSuccessful = isSuccessful;
            Data = data;
        }

        public override string ToString()
        {
            string data = "";
            foreach (Itinerary item in Data)
            {
                data = data + item.ToString();
            }
            return $"Message: {Message}\nIsSuccessful: {IsSuccessful}\nData: {data}";
        }
    }
}
