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
        public List<UserItinerary> Data { get; set; }

        public List<string> Emails { get; set; }

        public HyperlinkResponse(string message, bool isSuccessful, List<UserItinerary> data, List<string> emails)
        {
            Message = message;
            IsSuccessful = isSuccessful;
            Data = data;
            Emails = emails;
        }
    }
}
