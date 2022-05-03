using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.DBModels;

namespace Pentaskilled.MEetAndYou.Entities.Models
{
    public class UPData
    {
        private UserAccountRecord _userAccount;
        private List<Itinerary> itineraries;

        public UPData(UserAccountRecord user, List<Itinerary> itineraries)
        {
            this._userAccount = user;
            this.itineraries = itineraries;
        }

        public UserAccountRecord GetUserAccount()
        {
            return _userAccount; 
        }

        public List<Itinerary> getUserItineraries()
        {
            return itineraries;
        }
    }
}
