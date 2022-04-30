using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class SuggestionResponse : BaseResponse
    {
        private List<Event> _data;

        public List<Event> Data { get; set; }

        public SuggestionResponse() : base()
        {
            Data = new List<Event>();
        }

        public SuggestionResponse(string message, bool isSuccessful, List<Event> data)
        {
            Message = message;
            IsSuccessful = isSuccessful;
            Data = data;
        }
    }
}
