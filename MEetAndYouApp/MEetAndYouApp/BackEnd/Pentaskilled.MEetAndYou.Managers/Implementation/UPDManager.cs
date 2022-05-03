using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pentaskilled.MEetAndYou.Entities.Models;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Pentaskilled.MEetAndYou.DataAccess.Implementation;
using Microsoft.EntityFrameworkCore;


namespace Pentaskilled.MEetAndYou.Managers.Implementation
{
    public class UPDManager
    {
        private ItineraryDAO _itineraryDAO;
        private UserDAO _userDAO;
        
        
        public UPDManager()
        {
            this._itineraryDAO = new ItineraryDAO();
            this._userDAO = new UserDAO();
        }

        public UPDManager(ItineraryDAO iDAO, UserDAO userDAO)
        {
            this._itineraryDAO = iDAO;
            this._userDAO = userDAO;
        }

        /* Method to get that the list of itineraries associated with the user along with the 
           account information, both pieces of data are wrapped ina a UPData object.*/
        public async Task<UPData> GetUPData(int userID)
        {
            List<Itinerary> itineraries = this._itineraryDAO.GetUserItineraries(userID);
            UserAccountRecord user = await _userDAO.getUserAccount(userID);
            return new UPData(user, itineraries);
        }


    }
}
