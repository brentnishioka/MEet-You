using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class RatingResponse : BaseResponse
    {
        private List<UserEventRating> _data;

        public List<UserEventRating> Data { get; set; }

        public RatingResponse() : base()
        {
            Data = new List<UserEventRating>();
        }

        public RatingResponse(string message, bool isSuccessful, List<UserEventRating> data)
        {
            Message = message;
            IsSuccessful = isSuccessful;
            Data = data;
        }
    }
}
