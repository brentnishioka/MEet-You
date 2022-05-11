using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;


namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class UPDataResponse : BaseResponse
    {
        // might be error
        public UserAccountRecordResponse _userAccount {get; set; }
        public List<Itinerary> itineraries { get; set; }


        public UPDataResponse() : base()
        {
            _userAccount = null;
            itineraries = new List<Itinerary>();
        }

        public UPDataResponse(string message, bool isSuccessful, UserAccountRecordResponse user, List<Itinerary> itineraries)
        {
            this.Message = message;
            this.IsSuccessful = isSuccessful;
            this._userAccount = user;
            this.itineraries = itineraries;
        }



    }
}
